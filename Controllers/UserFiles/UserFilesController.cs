using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniDrive.Services;
using MiniDrive.Models;
using System.Threading.Tasks;
using MiniDrive.Services.Interfaces;


namespace MiniDrive.Controllers.UserFiles
{
    //[Authorize]
    public class UserFilesController : ControllerBase
    {
        private readonly IUserFileRepository _userFileRepository;

        public UserFilesController(IUserFileRepository userFileRepository)
        {
            _userFileRepository = userFileRepository;
        }

        [HttpGet]
        [Route("/api/userfiles")]
        public async Task<ActionResult<IEnumerable<UserFile>>> GetAll(int userId)
        {
            try
            {
                var (userFiles, message, statusCode) = await _userFileRepository.GetAll(userId);
                if (userFiles == null || userFiles == Enumerable.Empty<UserFile>())
                {
                    return NotFound(message);
                }
                
                return Ok(new
                {
                    Status = statusCode,
                    Message = message,
                    UserFiles = userFiles
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error obtaining userFiles: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("api/userfiles/{id}")]
        public async Task<IActionResult> GetById(int id, int userId)
        {
            try
            {
                var (userFile, message, statusCode) = await _userFileRepository.GetById(id, userId);
                if (userFile == null)
                {
                    return StatusCode((int)statusCode, new
                    {
                        status = statusCode,
                        message,
                        error = true
                    });
                }

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Archivo encontrado exitosamente",
                    error = false,
                    userFile
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