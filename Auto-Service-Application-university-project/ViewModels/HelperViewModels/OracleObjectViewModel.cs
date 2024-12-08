using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auto_Service_Application_university_project.Data;
using Auto_Service_Application_university_project.Models;

namespace Auto_Service_Application_university_project.ViewModels.HelperViewModels
{
    public class OracleObjectViewModel
    {
        private OracleRepository _oracleRepository;

        public OracleObjectViewModel()
        {
            _oracleRepository = new OracleRepository();
        }

        public async Task<ObservableCollection<OracleObject>> GetSystemObjectsAsync()
        {
            return await _oracleRepository.GetSystemObjectsAsync();
        }
    }
}
