using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure
{
    public abstract class BaseResponse
    {
        [XmlIgnore]
        private readonly Dictionary<string, string> headers = new Dictionary<string, string>();

        public virtual void AddHeader(string key, string value)
        {
            if (headers.Contains(new KeyValuePair<string, string>(key, value)))
                return;

            if (!headers.ContainsKey(key))
                headers.Add(key, value);
            else
                headers[key] = value;
        }

        public virtual string GetHeader(string key)
        {
            string outValue;

            return headers.TryGetValue(key, out outValue) ? outValue : string.Empty;
        }

        [XmlIgnore]
        public virtual Dictionary<string, string> DefaultResponseHeaders
        {
            get { return headers; }
        }
    }
}
