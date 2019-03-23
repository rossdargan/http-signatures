using System.Collections.Generic;

namespace HttpSignatures.Client.Entities
{
    public interface IRequest
    {
        string Method { get; }
        string Body { get; }
        IEnumerable<string> GetHeader(string header);        
        string Path { get; }
    }
}
