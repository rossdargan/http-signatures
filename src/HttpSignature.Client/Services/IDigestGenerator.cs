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
        /// Calculates a digest for the given request
        /// </summary>
        /// <param name="request">The data we will calculate the digest from</param>
        /// <returns>A string with the hashing algorithim prefixed at the start. E.g. "SHA256=xxxxxxxx"</returns>
        string CalculateDigest(Stream request);
    }
}
