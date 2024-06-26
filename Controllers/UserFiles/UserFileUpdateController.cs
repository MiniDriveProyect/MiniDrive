using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniDrive.DTOs;
using MiniDrive.Services.Interfaces;

namespace MiniDrive.Controllers.UserFiles
{
    [Authorize]
    public class UserFileUpdateController : ControllerBase
    {
        private readonly IUserFileRepository _userfileRepository;

        public UserFileUpdateController(IUserFileRepository userfileRepository)
        {
            _userfileRepository = userfileRepository;
        }

        [HttpPost]
        [Route("api/userfile")]
        public async Task<IActionResult> Update(int id, [FromBody] UserFileDTO userfileDTO)
        {
            try
            {
                var (userFile, message, statusCode) = await _userfileRepository.Update(id, userfileDTO);
                if (userFile == null)
                {
                    return NotFound(new
                    {
                        status = StatusCodes.Status404NotFound,
                        message = "Archivo no encontrada",
                        error = true
                    });
                }

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Archivo actualizada con éxito",
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
    }
}