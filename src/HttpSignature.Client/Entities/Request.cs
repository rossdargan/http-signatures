using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HttpSignatures.Client.Entities
{
    public class Request : IRequest
    {
        public string Method { get; set; }
        public Dictionary<string, IEnumerable<string>> Headers { get; } = new Dictionary<string, IEnumerable<string>>(StringComparer.CurrentCultureIgnoreCase);
        public string Body { get; set; }
        public IEnumerable<string> GetHeader(string header)
        {
            return Headers[header];
        }

        public void SetHeader(string header, IEnumerable<string> value)
        {
            Headers[header] = value;
        }

        public string Path { get; set; }

        public static IRequest FromHttpRequest(HttpRequestMessage httpRequestMessage)
        {
            var request = new Request()
            {
                Method = httpRequestMessage.Method.ToString(),
                Path = httpRequestMessage.RequestUri.PathAndQuery
            };
            foreach (var header in httpRequestMessage.Headers)
            {
                request.Headers.Add(header.Key.ToLowerInvariant(), header.Value);
            }
            return request;
        }
    }
}
