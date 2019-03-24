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
        [Obsolete("This really shouldn't be needed... don't know why though", false)]
        string HashAlgorithm { get; }
        /// <summary>
        /// Indicates if (request-target) should be included in the list of headers thats included as part of the signature string
        /// </summary>
        bool IncludePseduoHeaderInSigantureString { get; }
        /// <summary>
        /// If true we will add the signature header as the Authorization header (the scheme will be set to Signature). If false we will add the signature as a header of signature.
        /// </summary>
        bool UseSignatureAsAuthroizationHeader { get; }
    }
}
