using System;
using System.Collections.Generic;
using System.Text;

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
