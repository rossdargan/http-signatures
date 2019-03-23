using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSignatures.Client.Services
{
    public interface IStringSigner
    {
        string Sign(string input);
    }
}
