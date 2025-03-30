using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Repository
{
    /// <summary>
    /// Interface for Class 
    /// </summary>
    public interface IUserRepository
    {
        UserModel GetUserByEmailAndPassword(string email, string password);
        IEnumerable<UserModel> GetAllUser();
        UserModel GetUser(int? Id);
        UserModel Add(UserModel user);
        UserModel Update(UserModel user);
        UserModel Remove(int Id);
    }
}
