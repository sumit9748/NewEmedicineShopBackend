using Emedicine.DAL.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emedicine.BAL.Services
{
    public interface IService
    {
        Task<User> Authenticate(string email,string password);
    }
}
