using HttpSignatures.Client.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HttpSignatures.Client.Services
{
    public class HttpSignatureStringExtractor : IHttpSignatureStringExtractor
    {

        /// <summary>
        /// Create the header field string by concatenating the **lowercased** header field name 
        /// followed with an ASCII colon `:`, an ASCII space ` `, and the header field value. 
        /// Leading and trailing optional whitespace (OWS) in the header field value MUST be omitted 
        /// (as specified in RFC7230, Section 3.2.4). If there are multiple instances of the same header field, 
        /// all header field values associated with the header field MUST be concatenated, separated by a ASCII comma 
        /// and an ASCII space `, `, and used in the order in which they will appear in the transmitted HTTP message. 
        /// Any other modification to the header field value MUST NOT be made.
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public string ExtractSignatureString(IRequest request, ISignatureSpecification signatureAuth)
        {
            var headerStrings = signatureAuth.Headers.Select(header=> $"{CleanHeaderName(header)}: {string.Join(", ", GetHeaderValue(header, request))}");            
            return string.Join("\n", headerStrings);
        }
        private object CleanHeaderName(string header)
        {
            return header.ToLowerInvariant();
        }

        private IEnumerable<string> GetHeaderValue(string header, IRequest request)
        {
            if (header == "(request-target)")
            {
                //This defines the implementation of the pseduo header  https://tools.ietf.org/html/rfc7540#section-8.1.2.3
                return new List<string>() { request.Method.ToString().ToLower() + " " + request.Path };
            }
            else
            {
                return request.GetHeader(header);
            }            
        }
    }
}
