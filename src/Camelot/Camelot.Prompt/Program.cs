using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Camelot
{
    internal static class Program
    {
        private static void Main()
        {
            var html = new HttpDownloader("http://www.olx.pt", string.Empty, "Mozilla/5.0 (Windows NT 6.0; WOW64) AppleWebKit/534.27+ (KHTML, like Gecko) Version/5.0.4 Safari/533.20.27").GetPage();
            //var html = File.ReadAllText("C:/Users/r3db/Desktop/Page.html");
            //var css = new HttpDownloader("http://fonts.googleapis.com/css?family=Oswald:400,700,300", string.Empty, "Mozilla/5.0 (Windows NT 6.0; WOW64) AppleWebKit/534.27+ (KHTML, like Gecko) Version/5.0.4 Safari/533.20.27").GetPage();
            var css = File.ReadAllText(@"..\..\..\Resources\Reference-001.css");

            var cssParser = new CssParser(css);
            var selectors = cssParser.Parse();

            foreach (var item in selectors)
            {
                Console.WriteLine(item);
                //Console.WriteLine("-------------------------------------");
                Console.WriteLine("______________________________________");
            }

            Console.ReadLine();

            HtmlParser htmlParser = new HtmlParser(html);
            HtmlElement root = htmlParser.Parse();

            IterateNodes(root, 0);
            DrawPage(root);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void IterateNodes(HtmlElement node, int depth)
        {
            if (node.Kind == "#text" || node.Kind == "#comment" || node.Kind == "#declaration")
            {
                Write("{0}", new string(' ', depth * 4));
                Write("<s:{0}>", node.Kind, ConsoleColor.Red);
                if (node.Parent == null)
                {
                    Write(" <p:#!null>", ConsoleColor.Cyan);
                }
                else
                {
                    Write(" <p:{0}>", node.Parent.Kind, ConsoleColor.Cyan);
                }
                Write(" : ");
                Write("[{0}]", node.Content.Replace("\r", "\\r").Replace("\n", "\\n"), ConsoleColor.Cyan);
                Write(Environment.NewLine);
            }
            else
            {
                Write("{0}", new string(' ', depth * 4));
                Write("<s:{0}>", node.Kind, ConsoleColor.Red);
                if (node.Parent == null)
                {
                    Write(" <p:#!null>", ConsoleColor.Cyan);
                }
                else
                {
                    Write(" <p:{0}>", node.Parent.Kind, ConsoleColor.Cyan);
                }
                Write(Environment.NewLine);
            }

            foreach (var attribute in node.Attributes)
            {
                Write("{0}", new string(' ', (depth + 1) * 4));
                Write("[{0}]", attribute.Key, ConsoleColor.Yellow);
                Write(":");
                Write("[{0}]", attribute.Value, ConsoleColor.Green);
                Write(Environment.NewLine);
            }

            var newDepth = depth + 1;
            foreach (var item in node.GetDescendents())
            {
                IterateNodes(item, newDepth);
            }
        }

        private static void DrawPage(HtmlElement root)
        {
            var bmp = new Bitmap(800, 800);
            var g = Graphics.FromImage(bmp);

            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;

            using (var gp = new GraphicsPath())
            {
                DrawNodes(root, gp);
                g.FillPath(Brushes.Black, gp);
            }

            bmp.Save(@"C:\Users\r3db\Desktop\Html.png", ImageFormat.Png);
        }

        private static void DrawNodes(HtmlElement node, GraphicsPath path)
        {

            switch (node.Kind)
            {
                case "#text":
                case "#comment":
                {
                    path.AddString(node.Content, new FontFamily("Consolas"), (int)FontStyle.Regular, 14f, new Point(0, 0), StringFormat.GenericDefault);
                    break;
                }
                case "div":
                {
                    //path.AddRectangle(new Rectangle(node.Style.Top, node.Style.Left, node.Style.Width, node.Style.Height));
                    break;
                }
            }

            foreach (var item in node.GetDescendents())
            {
                DrawNodes(item, path);
            }
        }

        private static void Write(object o)
        {
            Console.Write(o);
        }

        private static void Write(string format, object o)
        {
            Console.Write(format, o);
        }

        private static void Write(string format, object o, ConsoleColor color)
        {
            var backup = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(format, o);
            Console.ForegroundColor = backup;
        }
    }
}