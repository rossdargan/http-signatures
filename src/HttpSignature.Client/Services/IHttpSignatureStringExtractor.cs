using HttpSignatures.Client.Entities;

namespace HttpSignatures.Client.Services
{
    /// <summary>
    /// Used to generate the string that will be signed and attached to the request. <typeparamref name="ISignatureSpecification"/> indicates
    /// which headers will be included in the signature string.
    /// The pseudo header `(request-target)` may be included in the specification as a header if you wish to sign the http request target.
    /// </summary>
    /// <remarks>
    /// The specification can be found here: https://tools.ietf.org/id/draft-cavage-http-signatures-08.html#canonicalization
    /// </remarks>
    public interface IHttpSignatureStringExtractor
    {
        /// <summary>
        /// Takes the given request and the signature specification and generates the headers
        /// </summary>
        /// <param name="request"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        string ExtractSignatureString(IRequest request, ISignatureSpecification signatureSpecification);
    }
}
