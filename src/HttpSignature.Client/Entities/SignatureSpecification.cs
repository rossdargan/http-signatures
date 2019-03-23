using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSignatures.Client.Entities
{
    public class SignatureSpecification : ISignatureSpecification
    {
        public SignatureSpecification()
        {
            Realm = "default";
        }

        public string KeyId
        {
            get;
            set;
        }

        public IEnumerable<string> Headers
        {
            get;
            set;
        }


        public string Algorithm { get; set; }
        public string Realm { get; set; }


        public string HashAlgorithm
        {
            get
            {
                return Algorithm.Split('-')[1];
            }
        }

        public string SignatureHeaderName { get; set; }
    }
}
