using HttpSignatures.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpSignatures.Client.Services
{
    public class HttpSignatureStringExtractor : IHttpSignatureStringExtractor
    {
        public string ExtractSignatureString(IRequest request, ISignatureSpecification signatureAuth)
        {
            var headerStrings = signatureAuth.Headers.SelectMany(header=> GetHeaderValue(header, request).Select(value=> $"{header}: {value}"));            
            return string.Join("\n", headerStrings);
        }

        private IEnumerable<string> GetHeaderValue(string header, IRequest request)
        {
            if (header == "(request-target)")
            {
                return new List<string>() { request.Method.ToString().ToLower() + " " + request.Path };
            }
            else
            {
                return request.GetHeader(header);
            }            
        }
    }
}
