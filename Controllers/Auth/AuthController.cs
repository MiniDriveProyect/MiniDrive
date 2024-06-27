using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniDrive.DTOs;
using MiniDrive.Services.Interfaces;

namespace MiniDrive.Controllers.Auth
{
    
    public class AuthController : ControllerBase
    {
        private readonly  IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        [HttpPost]
        [Route ("api/auth")]
        
        public async Task<IActionResult> login(UserDTO userDTO)
        {

            if(!ModelState.IsValid)
                return BadRequest(new {statusCode = StatusCodes.Status400BadRequest, message = "Some required fields are empty!"});
           
            try{
                var user = await _authRepository.Login(userDTO);
                if(user == null)
                    return BadRequest(new {statusCode = StatusCodes.Status400BadRequest, message = "Invalid Credentials"});

                var token  = _authRepository.generateToken(user);

                return Ok(new {statusCode = StatusCodes.Status200OK, token});

            }catch(Exception e){

                return StatusCode(StatusCodes.Status500InternalServerError, new {StatusCode = StatusCodes.Status500InternalServerError, message ="Internal Server Error"});
            }
        }
       
    }
}