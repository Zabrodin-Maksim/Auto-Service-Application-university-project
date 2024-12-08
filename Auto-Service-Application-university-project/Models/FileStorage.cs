using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class FileStorage
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileExtension { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string OperationPerformed { get; set; }
        public byte[] FileContent { get; set; }
    }
}
