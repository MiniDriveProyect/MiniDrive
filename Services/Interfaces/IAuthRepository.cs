using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniDrive.DTOs;
using MiniDrive.Models;

namespace MiniDrive.Services.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user);
        Task<User> Login(UserDTO user);
       string generateToken(User user);
    }
}