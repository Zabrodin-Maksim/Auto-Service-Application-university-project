using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IFileStorageRepository
    {
        IEnumerable<FileStorage> GetAllFiles();
        FileStorage GetFileByName(string fileName);
        void AddFile(FileStorage file, User currentUser);
        void UpdateFile(FileStorage file, User currentUser);
        void DeleteFile(string fileName, User currentUser);
    }
}
