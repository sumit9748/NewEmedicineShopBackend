using Emedicine.BAL.Services;
using Emedicine.BAL.UserBased;
using Emedicine.DAL.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
namespace Emedicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager md;
        private readonly IService sr;
        public UserController(IUserManager _md, IService _sr) 
        {
            md= _md;
            sr = _sr;
        }
        [HttpGet]
        [Route("AllUser")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        //To get all user..
        public Task<IEnumerable<User>> GetAllUser()
        {
            try
            {
                return md.GetAllUser();

            }
            catch (Exception ex)
            {
                IEnumerable<User> users = new List<User>();
                return (Task<IEnumerable<User>>)users;
            }
        }
        [HttpGet]
        [Route("OnlineUser")]
        [EnableCors("AllowOrigin")]

        //To get those user which are online
        public Task<IEnumerable<User>> GetOnlineUser()
        {
            try
            {
                return md.GetOnlineUser();

            }catch(Exception ex)
            {
                IEnumerable<User> users = new List<User>() ;
                return (Task<IEnumerable<User>>)users;
            }
        }
        [HttpGet("{id}")]
        [EnableCors("AllowOrigin")]
        [Authorize]
        //Get a particular user with his id
        public async Task<IActionResult> GetUser(int id) {
            try
            {
                var user=await md.GetUser(id);
                if(user==null) return BadRequest("user id not exists");
                return Ok(user);
            }
            catch (Exception es)
            {
                return BadRequest("something went erong");
            }
        }
        [HttpPost("Register")]
        [EnableCors("AllowOrigin")]

        //Register a new user..
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                if (user == null)
                    return BadRequest();
                if (await md.AddUser(user))
                {
                    return StatusCode(
                       StatusCodes.Status200OK,
                       "User added successfully");
                } 
                else return StatusCode(StatusCodes.Status406NotAcceptable, "User already exists");
            }
            catch(Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Something went wrong");
            }
            
        }
        
        [HttpPut("{id}")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        //Update a partcular user with his id
        public async Task<IActionResult> UpdateUser(int id,[FromBody] User user)
        {
            try
            {
                User oldUser=await md.GetUser(id);
                if (oldUser == null) return NotFound();
                oldUser.FirstName = user.FirstName;
                oldUser.LastName = user.LastName;
                oldUser.Email = user.Email;
                oldUser.Password= user.Password;
                oldUser.Status= user.Status;
                oldUser.Address = user.Address;
                oldUser.Type = user.Type;
                md.UpdateUser(oldUser);
                return Ok("User updated successfully");
            }catch( Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error updating user");
            }
        }
        [HttpDelete("{id}")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        //Delete a particular user with his id
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                User oldUser = await md.GetUser(id);
                if(oldUser==null) return NotFound();
                md.DeleteUser(oldUser);
                return Ok("User removed successfully");
            }
            catch (Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error updating user");
            }

        }
        //logged in a user
        [HttpPost("Login")]
        [EnableCors("AllowOrigin")]

        public async Task<IActionResult> LoginUser([FromBody]UserLogin userLogin)
        {
            if (userLogin == null)
            {
                return null;
            }
            else
            {
                var user=await sr.Authenticate(userLogin.Email, userLogin.Password);
                if (user == null) return BadRequest("Not Found any user");
                else return Ok(user);
            }
        }
        

    }
}
