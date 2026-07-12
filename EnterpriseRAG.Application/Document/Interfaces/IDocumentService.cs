using EnterpriseRAG.Application.Document.DTO;
using Microsoft.AspNetCore.Http;

namespace EnterpriseRAG.Application.Document.Interfaces
{
    public interface IDocumentService
    {
        Task<UploadDocumentResponse> UploadAsync(IFormFile file);

    }
}
