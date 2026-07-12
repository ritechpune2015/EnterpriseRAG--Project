using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Document.DTO
{
    public class UploadDocumentResponse
    {
        public Guid DocumentId { get; set; }
        public string FileName { get; set; } = "";
        public long FileSize { get; set; }
        public string Message { get; set; } = "";
    }

}
