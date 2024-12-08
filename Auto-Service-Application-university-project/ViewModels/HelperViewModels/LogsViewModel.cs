using Auto_Service_Application_university_project.Data;
using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.ViewModels.HelperViewModels
{
    public class LogsViewModel 
    {
        private LogsRepository logsRepository;

        public LogsViewModel()
        {
            logsRepository = new LogsRepository();
        }

        public async Task<ObservableCollection<Logs>> GetAllLogsAsync()
        {
            return await logsRepository.GetAllLogsAsync();
        }
    }
}
