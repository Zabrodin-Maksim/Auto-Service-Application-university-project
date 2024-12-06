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

        public async Task<ObservableCollection<User>> GetAllUsers()
        {
           return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> Authorization(string userName, string userPassword)
        {
            return await _userRepository.AuthenticateUserAsync(userName, userPassword);
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUser(int userId)
        {
            await _userRepository.DeleteUserAsync(userId);
        }

        public async Task AssignRole(int userId, int roleId)
        {
            await _userRepository.AssignRoleAsync(userId, roleId);
        }

        public async Task InsertEmployer(int userId, int officeId, string speciality)
        {
            await _userRepository.InsertEmployerAsync(userId, officeId, speciality);
        }

        public async Task<Employer> GetEmployerByPhone(long phone)
        {
            return await _userRepository.GetEmployerByPhoneAsync(phone);
        }

        public async Task<Employer> GetEmployer(int employerId)
        {
            return await _userRepository.GetEmployerAsync(employerId);
        }
    }
}
