using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Models
{
    public class User
    {
        public int UserId { get; set; }              // Уникальный идентификатор пользователя
        public string Username { get; set; }         // Имя пользователя (логин)
        public string PasswordHash { get; set; }     // Хэш пароля
        public string Role { get; set; }             // Роль пользователя
        public bool IsActive { get; set; }           // Статус активности

        // Метод для проверки прав доступа
        public bool HasPermission(string permission)
        {
            // Реализация проверки прав доступа на основе роли
            // Предполагается, что есть словарь ролей и разрешений
            // Например:
            var rolePermissions = new Dictionary<string, List<string>>
            {
                { "Admin", new List<string> { "AddClient", "UpdateClient", "DeleteClient", /* другие права */ } },
                { "User", new List<string> { "AddClient" } }
            };

            if (rolePermissions.ContainsKey(Role))
            {
                return rolePermissions[Role].Contains(permission);
            }

            return false;
        }
    }
}
