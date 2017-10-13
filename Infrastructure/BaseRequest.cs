using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure
{
    public abstract class BaseRequest
    {
        private const string headerProtocolo = "Protocolo";

        [XmlIgnore]
        private readonly Dictionary<string, string> headers = new Dictionary<string, string>();

        internal HttpRequestMessage HttpRequestMessage { get; set; }

        [XmlIgnore]
        public virtual Dictionary<string, string> DefaultRequestHeaders { get { return headers; } }

        public string Protocolo { get { return GetHeader(headerProtocolo); } }

        public virtual void AddHeader(string key, string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

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

        public void SetHttpRequestMessage(HttpRequestMessage httpRequestMessage)
        {
            if (HttpRequestMessage == null)
                HttpRequestMessage = httpRequestMessage;
        }

        public void SetHttpRequestMessageBasedOnAnotherRequestMessage(BaseRequest request)
        {
            if (HttpRequestMessage == null)
            {
                HttpRequestMessage = request.HttpRequestMessage;

                Guid protocolo;
                if (Guid.TryParse(request.GetHeader(headerProtocolo), out protocolo))
                    AddHeader(headerProtocolo, protocolo.ToString());
            }
        }

        private Guid? ObterProtocoloDoHeader(HttpRequestMessage request)
        {
            Guid? protocolo = null;

            IEnumerable<string> protocolHeaderValues;

            if (request.Headers.TryGetValues(headerProtocolo, out protocolHeaderValues)
                && protocolHeaderValues != null
                && protocolHeaderValues.Any())
            {
                string protocoloStr = protocolHeaderValues.FirstOrDefault();

                Guid protocoloTry;
                if (Guid.TryParse(protocoloStr, out protocoloTry))
                    protocolo = protocoloTry;
            }

            return protocolo;
        }
    }
}
