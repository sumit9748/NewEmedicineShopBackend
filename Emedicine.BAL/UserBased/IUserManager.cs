using Emedicine.DAL.model;


namespace Emedicine.BAL.UserBased
{
    public interface IUserManager
    {
        public Task<IEnumerable<User>> GetAllUser();
        public Task<IEnumerable<User>> GetOnlineUser();
        public Task<User> GetUser(int id);
        public Task<bool> AddUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(User user);
        public Task<User> LoginUser(string username, string pass);

    }
}
