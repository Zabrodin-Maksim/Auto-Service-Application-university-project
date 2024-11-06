using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IFileStorageRepository
    {
        IEnumerable<FileStorage> GetAllFiles();
        FileStorage GetFileByName(string fileName);
        void AddFile(FileStorage file, User currentUser);
        void UpdateFile(FileStorage file, User currentUser);
        void DeleteFile(string fileName, User currentUser);
    }
}
