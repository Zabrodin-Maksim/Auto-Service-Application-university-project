using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        #region Private Fields
        private readonly MainViewModel _mainViewModel;

        #endregion


        #region Properties


        #endregion

        #region Commands


        #endregion

        public RegistrationViewModel(MainViewModel mainViewModel) 
        {
            _mainViewModel = mainViewModel;
        }
    }
}
