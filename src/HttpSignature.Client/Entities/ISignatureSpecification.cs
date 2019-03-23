using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSignatures.Client.Entities
{
    public interface ISignatureSpecification
    {
        string KeyId { get; }
        IEnumerable<string> Headers { get; }
        string Algorithm { get; }
        string HashAlgorithm { get; }
        string SignatureHeaderName { get; }
    }
}
