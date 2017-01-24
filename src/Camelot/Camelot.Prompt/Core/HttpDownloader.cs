using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Camelot
{
    public class HttpDownloader
    {
        #region Internal Instance Data

        private readonly string referer;
        private readonly string userAgent;
        private Encoding encoding;
        private WebHeaderCollection headers;
        private Uri url;

        #endregion

        public HttpDownloader(string url, string referer, string userAgent)
        {
            encoding = Encoding.GetEncoding("ISO-8859-1");
            this.url = new Uri(url); // verify the uri
            this.userAgent = userAgent;
            this.referer = referer;
        }

        public string GetPage()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            if (!string.IsNullOrEmpty(referer))
            {
                request.Referer = referer;
            }

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }

            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                headers = response.Headers;
                url = response.ResponseUri;
                return ProcessContent(response);
            }

        }

        private string ProcessContent(HttpWebResponse response)
        {
            SetEncodingFromHeader(response);

            Stream s = response.GetResponseStream();

            if (response.ContentEncoding.ToLower().Contains("gzip"))
            {
                s = new GZipStream(s, CompressionMode.Decompress);
            }
            else if (response.ContentEncoding.ToLower().Contains("deflate"))
            {
                s = new DeflateStream(s, CompressionMode.Decompress);
            }

            MemoryStream memStream = new MemoryStream();
            int bytesRead;
            byte[] buffer = new byte[0x1000];
            for (bytesRead = s.Read(buffer, 0, buffer.Length); bytesRead > 0; bytesRead = s.Read(buffer, 0, buffer.Length))
            {
                memStream.Write(buffer, 0, bytesRead);
            }
            s.Close();
            string html;
            memStream.Position = 0;
            using (StreamReader r = new StreamReader(memStream, encoding))
            {
                html = r.ReadToEnd().Trim();
                html = CheckMetaCharSetAndReencode(memStream, html);
            }

            return html;
        }

        private void SetEncodingFromHeader(HttpWebResponse response)
        {
            string charset = null;
            if (string.IsNullOrEmpty(response.CharacterSet))
            {
                Match m = Regex.Match(response.ContentType, @";\s*charset\s*=\s*(?<charset>.*)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    charset = m.Groups["charset"].Value.Trim('\'', '"');
                }
            }
            else
            {
                charset = response.CharacterSet;
            }

            if (!string.IsNullOrEmpty(charset))
            {
                try
                {
                    encoding = Encoding.GetEncoding(charset);
                }
                catch (ArgumentException)
                {
                }
            }
        }

        private string CheckMetaCharSetAndReencode(Stream memStream, string html)
        {
            Match m = new Regex(@"<meta\s+.*?charset\s*=\s*(?<charset>[A-Za-z0-9_-]+)", RegexOptions.Singleline | RegexOptions.IgnoreCase).Match(html);
            if (m.Success)
            {
                string charset = m.Groups["charset"].Value.ToLower() ?? "iso-8859-1";

                if ((charset == "unicode") || (charset == "utf-16"))
                {
                    charset = "utf-8";
                }

                try
                {
                    Encoding metaEncoding = Encoding.GetEncoding(charset);

                    if (encoding.Equals(metaEncoding) == false)
                    {
                        memStream.Position = 0L;
                        StreamReader recodeReader = new StreamReader(memStream, metaEncoding);
                        html = recodeReader.ReadToEnd().Trim();
                        recodeReader.Close();
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return html;
        }
    }
}