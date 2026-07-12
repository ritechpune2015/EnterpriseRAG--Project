using DocumentFormat.OpenXml.Office2010.Word;
using EnterpriseRAG.Application.Document.DTO;
using EnterpriseRAG.Application.Document.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EnterpriseRAG.Infrastructure.Documents
{
    public class DocumentService : IDocumentService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IDocumentReaderFactory _readerFactory;
        private readonly IChunkingService _chunkingService
        ;
        public DocumentService(
            IWebHostEnvironment environment, IDocumentReaderFactory readerFactory, IChunkingService chunkingService)
        {
            _environment = environment;
            _readerFactory = readerFactory;
            _chunkingService = chunkingService;
        }

        private readonly string[] allowedExtensions ={
    ".pdf",
    ".docx",
    ".txt",
    ".csv"
};

        public async Task<UploadDocumentResponse> UploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("No file selected.");

            var extension =
                Path.GetExtension(file.FileName)
                    .ToLower();

            if (!allowedExtensions.Contains(extension))
                throw new Exception("Unsupported file.");

            var folder =
                Path.Combine(
                    _environment.ContentRootPath,
                    "Uploads",
                    "Documents");

            Directory.CreateDirectory(folder);

            var fileName =
                $"{Guid.NewGuid()}{extension}";

            var filePath =
                Path.Combine(folder, fileName);

            using (var stream =
                new FileStream(filePath,
                FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var reader = _readerFactory.GetReader(extension);

            string content = await reader.ReadAsync(filePath);
            //Console.WriteLine(content);
            var documentId = Guid.NewGuid();
            var chunks =_chunkingService.ChunkDocument(documentId,content);

            foreach (var chunk in chunks)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine(chunk.Content);
                Console.WriteLine("--------------------------------");
            }


            return new UploadDocumentResponse
            {
                DocumentId = documentId,
                FileName = fileName,
                FileSize = file.Length,
                Message = "Document uploaded successfully."
            };
        }

    }
}