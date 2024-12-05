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
    public class SparePartViewModel
    {
        private SparePartRepository _repository;

        public SparePartViewModel()
        {
            _repository = new SparePartRepository();
        }

        public async Task AddSparePart(SparePart sparePart)
        {
            await _repository.InsertSparePartAsync(sparePart);
        }

        public async Task UpdateSparePart(SparePart sparePart)
        {
            _repository.UpdateSparePartAsync(sparePart);
        }

        public async Task DeleteSparePart(int sparePartId)
        {
            _repository.DeleteSparePartAsync(sparePartId);
        }

        public async Task<SparePart> GetSparePartById(int sparePartId)
        {
            return await _repository.GetSparePartByIdAsync(sparePartId);
        }

        public async Task<ObservableCollection<SparePart>> GetAllSpareParts()
        {
            return await _repository.GetAllSparePartsAsync();
        }

        public async Task<ObservableCollection<SparePart>> GetSparePartsByOffice(int officeId)
        {
            return await _repository.GetSparePartsByOfficeAsync(officeId);
        }
    }
}
