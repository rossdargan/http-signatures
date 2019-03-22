using HttpSignatures.Client.Entities;

namespace HttpSignatures.Client.Services
{
    public interface ISignatureGenerator
    {
        string GenerateSignature(ISignatureSpecification signatureSpecification, IRequest request);
    }
}
