using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpSignatures.Client.Services
{
    public interface IDigestGenerator
    {
        /// <summary>
        /// Calculates a digest for the given request. An implementation fo the spec here: https://tools.ietf.org/html/rfc3230#section-4.3.2
        /// </summary>
        /// <param name="request">The data we will calculate the digest from</param>
        /// <example>
        /// This shows how you might get the stream from a HttpRequestMessage
        /// string digest = _digestGenerator.CalculateDigest(await request.Content.ReadAsStreamAsync());
        /// </example>
        /// <returns>A string with the hashing algorithim prefixed at the start. E.g. "SHA256=xxxxxxxx"</returns>        
        string CalculateDigest(Stream request);
    }
}
