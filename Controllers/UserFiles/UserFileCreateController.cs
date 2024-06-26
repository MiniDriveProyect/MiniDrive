using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniDrive.DTOs;
using MiniDrive.Models;
using MiniDrive.Services.Interfaces;

namespace MiniDrive.Controllers.UserFiles
{
    public class UserFileCreateController : ControllerBase
    {
        private readonly IUserFileRepository _userfileRepository;

        public UserFileCreateController(IUserFileRepository userfileRepository)
        {
            _userfileRepository = userfileRepository;
        }

        [HttpPost]
        [Route("api/userfile")]
        public async Task<ActionResult<UserFile>> PostMarketing( [FromBody] UserFileDTO userfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "La solicitud contiene datos no válidos.",
                    error = true,
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }
            try{  
                await _userfileRepository.Add(userfile);
                return Created("", new
                {
                    status = StatusCodes.Status201Created,
                    message = "Archivo creado exitosamente",
                    error = false
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = StatusCodes.Status500InternalServerError,
                    message = "Error inesperado.",
                    error = true,
                    errorMessage = ex.Message
                });
            }
        }

    }
}