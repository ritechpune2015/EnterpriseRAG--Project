using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = "";
        public string OriginalFileName { get; set; } = "";
        public string ContentType { get; set; } = "";
        public string FilePath { get; set; } = "";
        public long FileSize { get; set; }
        public DateTime UploadedOn { get; set; }
        public string UploadedBy { get; set; } = "Admin";
    }
}
