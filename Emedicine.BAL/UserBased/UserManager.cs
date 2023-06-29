using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.model;

namespace Emedicine.BAL.UserBased
{
    public class UserManager : IUserManager
    {
        private readonly IDataAccess _da;
        public UserManager(IDataAccess da)
        {
            _da = da;
        }
        //Get all user
        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _da.user.GetAllAsync();
        }
        //Get all online user
        public async Task<IEnumerable<User>> GetOnlineUser()
        {
            return await _da.user.GetAllListAsync(x => x.Status == "online");
        }
        //Get user by id
        public async Task<User> GetUser(int id)
        {
            return await _da.user.GetFirstOrDefaultAsync(x => x.Id == id);
        }
        //Add user
        public async Task<bool> AddUser(User user)
        {

            if (user == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                //get all user 
                IEnumerable<User> users = await _da.user.GetAllAsync();
                //check if same user is prsent in the databse or not to maintain the uniqeness
                if (users.Any(users => users.Id == user.Id || users.Email == user.Email))
                {
                    return await Task.FromResult(false);
                }
                _da.user.AddAsync(user);
                _da.save();
                return await Task.FromResult(true);
            }
        }
        //update a user
        public void UpdateUser(User user)
        {
            _da.user.UpdateExisting(user);
            _da.save();
        }
        //delete a user.
        public void DeleteUser(User user)
        {
            _da.user.Remove(user);
            _da.save();
        }
        //Login user by checking its username and password
        public async Task<User> LoginUser(string username,string pass)
        {
            if (username == null)
            {
                return null;
            }
            else
            {
                var user = await _da.user.GetFirstOrDefaultAsync(u => u.Email == username);
                if (user.Password == pass) return user;
                else return null;
            }
        }
    }
}