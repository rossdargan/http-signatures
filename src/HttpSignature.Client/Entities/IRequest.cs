using System.Collections.Generic;

namespace HttpSignatures.Client.Entities
{
    public interface IRequest
    {
        /// <summary>
        /// The method of the request (GET/PUT/POST etc)
        /// </summary>
        string Method { get; }
        /// <summary>
        /// Returns the header values from the string. Note it should do this in a case insensitive way.
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        IEnumerable<string> GetHeader(string header);        
        /// <summary>
        /// The URL path and query of the request
        /// </summary>
        string Path { get; }
    }
}
