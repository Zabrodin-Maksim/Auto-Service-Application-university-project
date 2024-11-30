using Auto_Service_Application_university_project.Data;
using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.ViewModels.HelperViewModels
{
    public class UserViewModel
    {
        private UserRepository _userRepository;
        
        public UserViewModel()
        {
            _userRepository = new UserRepository();
        }

        public async Task AddNewUser(User newUser)
        {
            await _userRepository.AddUser(newUser);
        }

        public async Task Authorization(string userName, string userPassword)
        {
            await _userRepository.AuthenticateUserAsync(userName, userPassword);
        }
    }
}
