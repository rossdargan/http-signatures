using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSignatures.Client.Entities
{
    public class SignatureSpecification : ISignatureSpecification
    {
        public string KeyId
        {
            get;
            set;
        }

        public IEnumerable<string> Headers { get; set;  }

        public string Algorithm { get; set; }        

        public string HashAlgorithm { get; set; }


        public bool IncludePseduoHeaderInSigantureString { get; set; } = true;

        public bool UseSignatureAsAuthroizationHeader { get; set; } = true;
    }
}
