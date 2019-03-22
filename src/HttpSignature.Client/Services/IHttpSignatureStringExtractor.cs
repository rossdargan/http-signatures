using HttpSignatures.Client.Entities;

namespace HttpSignatures.Client.Services
{
    public interface IHttpSignatureStringExtractor
    {
        string ExtractSignatureString(IRequest request, ISignatureSpecification spec);
    }
}
