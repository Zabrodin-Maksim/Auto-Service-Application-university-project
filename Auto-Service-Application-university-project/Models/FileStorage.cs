using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Models
{
    public class FileStorage
    {
        public int FileId { get; set; }                   // Уникальный идентификатор файла
        public string FileName { get; set; }              // Имя файла
        public string FileType { get; set; }              // Тип файла (например, "Image", "Document")
        public string FileExtension { get; set; }         // Расширение файла (например, ".jpg", ".pdf")
        public DateTime UploadDate { get; set; }          // Дата загрузки файла
        public DateTime ModificationDate { get; set; }    // Дата последнего изменения
        public string OperationPerformed { get; set; }    // Операция, выполненная над файлом
        public byte[] FileContent { get; set; }           // Содержимое файла в виде массива байтов
    }
}
