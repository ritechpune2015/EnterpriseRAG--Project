using DocumentFormat.OpenXml.Office2010.Word;
using EnterpriseRAG.Application.Document.DTO;
using EnterpriseRAG.Application.Document.Interfaces;
using EnterpriseRAG.Application.Embeddings.Interfaces;
using EnterpriseRAG.Application.Qdrant;
using EnterpriseRAG.Infrastructure.Embedding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EnterpriseRAG.Infrastructure.Documents
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentReaderFactory _readerFactory;
        private readonly IChunkingService _chunkingService;
        private readonly IEmbeddingService _embeddingService;
        private readonly IQdrantService _qdrantService;
        public DocumentService(
            IDocumentReaderFactory readerFactory, IChunkingService chunkingService,IEmbeddingService embeddingService,IQdrantService qdrantService)
        {
            _readerFactory = readerFactory;
            _chunkingService = chunkingService;
            _embeddingService = embeddingService;
            _qdrantService = qdrantService;
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
                    AppContext.BaseDirectory,
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

            //foreach (var chunk in chunks)
            //{
            //    Console.WriteLine("--------------------------------");
            //    Console.WriteLine(chunk.Content);
            //    Console.WriteLine("--------------------------------");
            //}

            foreach (var chunk in chunks)
            {
                chunk.Embedding = await _embeddingService.GenerateEmbeddingAsync(chunk.Content);
            }

            /// store it in qdrant

            await this._qdrantService.IndexChunksAsync(chunks);

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