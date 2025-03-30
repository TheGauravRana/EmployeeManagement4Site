using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.Repository;

namespace EmployeeManagement.DTO
{
    /// <summary>
    /// repo. for Contoleers to saperate the logic creation and to use as DI
    /// </summary>
    public class MockUserRepository : IUserRepository
    {
        private readonly List<UserModel> _userList;

        /// <summary>
        /// user data hardcoded can be updated at runtime
        /// </summary>
        public MockUserRepository()
        {

            _userList = new List<UserModel>()
            {
                new UserModel { Id = 1, Name = "Admin", Email = "admin@example.com", Password = "admin" },
            };
        }
        //Get Uset by email and pass for login
        public UserModel GetUserByEmailAndPassword(string email, string password)
        {
            return _userList.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Password == password);
        }
        //Get All user 
        public IEnumerable<UserModel> GetAllUser()
        {
            return _userList;
        }
        //Get User with ID
        public UserModel GetUser(int? Id)
        {
            return _userList.FirstOrDefault(ur => ur.Id == Id);
        }
        //Add new user in runtime memory
        public UserModel Add(UserModel user)
        {
            user.Id = _userList.Max(e => e.Id) + 1;
            _userList.Add(user);
            return user;
        }
        //Update user
        public UserModel Update(UserModel userUpdate)
        {
            var usr = GetUser(userUpdate.Id);
            if (userUpdate != null)
            {
                usr.Name = userUpdate.Name;
                usr.Email = userUpdate.Email;
                usr.Password = userUpdate.Password;
            }

            return usr;
        }
        //Delete User
        public UserModel Remove(int Id)
        {
            var UrRemove = GetUser(Id);

            if (UrRemove != null)
            {
                _userList.Remove(UrRemove);
            }

            return UrRemove;
        }

    }
}
