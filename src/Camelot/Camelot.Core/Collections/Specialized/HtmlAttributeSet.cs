using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Camelot.Collections.Specialized
{
    public sealed class HtmlAttributeSet : IEnumerable<HtmlAttribute>
    {
        #region Internal Instance Data

        private readonly IDictionary<string, HtmlAttribute> set = new Dictionary<string, HtmlAttribute>();

        #endregion

        #region Methods

        internal void Add(HtmlAttribute attribute)
        {
            if (set.ContainsKey(attribute.Key))
            {
                set[attribute.Key] = attribute;
            }
            else
            {
                set.Add(new KeyValuePair<string, HtmlAttribute>(attribute.Key, attribute));   
            }
        }

        #endregion

        #region IEnumerator<HtmlAttribute> Implementation

        public IEnumerator<HtmlAttribute> GetEnumerator()
        {
            return set.Select(x => x.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}