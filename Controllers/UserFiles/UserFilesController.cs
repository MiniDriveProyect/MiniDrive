using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniDrive.Services;
using MiniDrive.Models;
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
        [Route("/api/userFiles")]
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error obtaining user files: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("api/userFiles/{id}")]
        public async Task<IActionResult> GetById(int id, int userId)
        {
            try
            {
                var (userFile, message, statusCode) = await _userFileRepository.GetById(id, userId);
                if (userFile == null)
                {
                    return NotFound(new
                    {
                        Status = statusCode,
                        Message = message,
                        Error = true
                    });
                }

                return Ok(new
                {
                    Status = statusCode,
                    Message = message,
                    UserFile = userFile,
                    Error = false
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Internal Server Error",
                    Error = true,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}