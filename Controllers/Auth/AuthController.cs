using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniDrive.DTOs;
using MiniDrive.Services;
using MiniDrive.Models;
//using MiniDrive.DTOs;
using System.Threading.Tasks;
using MiniDrive.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MiniDrive.Controllers.Auth
{
    
    public class AuthController : ControllerBase
    {
    
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        [HttpPost]
        [Route ("api/auth")]
        
        public async Task<IActionResult> Login([FromBody]UserDTO userDTO)
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

                return StatusCode(StatusCodes.Status500InternalServerError, new {StatusCode = StatusCodes.Status500InternalServerError, message ="Internal Server Error_ " + e.Message});
            }
        }
       
       [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {

            ModelState.Remove(nameof(user.Id));
            ModelState.Remove(nameof(user.UserFiles));
            ModelState.Remove(nameof(user.Folders));

            if(!ModelState.IsValid)
                return BadRequest(new {statusCode = StatusCodes.Status400BadRequest, message = "Some required fields are empty!"});
            try
            {
                var result = await _authRepository.Register(user);
                if (result == null)
                {
                    return BadRequest(new
                    {
                        status = StatusCodes.Status400BadRequest,
                        message = "No se registro!",
                        error = true
                    });
                }

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Usuario registrado con éxito",
                    error = false
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = StatusCodes.Status500InternalServerError,
                    message = "Error interno del servidor",
                    error = true,
                    errorMessage = ex.Message
                });
            }
        } 

/*         [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var result = await _authRepository.Login(loginDTO);
                if (!result.Success)
                {
                    return Unauthorized(new
                    {
                        status = StatusCodes.Status401Unauthorized,
                        message = result.Message,
                        error = true
                    });
                }

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Inicio de sesión exitoso",
                    token = result.Token,
                    error = false
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = StatusCodes.Status500InternalServerError,
                    message = "Error interno del servidor",
                    error = true,
                    errorMessage = ex.Message
                });
            }
        } */
    }
}
