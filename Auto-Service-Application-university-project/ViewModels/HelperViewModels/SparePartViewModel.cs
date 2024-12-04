using Auto_Service_Application_university_project.Data;
using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.ViewModels.HelperViewModels
{
    public class SparePartViewModel
    {
        private SparePartRepository _repository;

        public SparePartViewModel()
        {
            _repository = new SparePartRepository();
        }

        public async Task InsertSparePart(SparePart sparePart)
        {
            await _repository.InsertSparePartAsync(sparePart);
        }
    }
}
