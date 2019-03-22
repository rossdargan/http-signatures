using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSignatures.Client.Services
{
    public interface ISignatureStringGenerator
    {
        /// <summary>
        /// Generates a string by combinging all of the passed in data.
        /// </summary>
        /// <param name="httpMethod">The http method type (GET/POST/PUT etc)</param>
        /// <param name="path">The url path</param>
        /// <param name="headers">The headers to include in the signature string</param>
        /// <returns>The string to be signed</returns>
        string GenerateSignatureString(string httpMethod, string path, IEnumerator<KeyValuePair<string, IEnumerable<string>>> headers);
    }
}
