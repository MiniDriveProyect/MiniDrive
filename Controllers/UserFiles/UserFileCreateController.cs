using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniDrive.DTOs;
using MiniDrive.Models;
using MiniDrive.Services.Interfaces;

namespace MiniDrive.Controllers.UserFiles
{
    public class UserFileCreateController : ControllerBase
    {
        private readonly IUserFileRepository _userFileRepository;

        public UserFileCreateController(IUserFileRepository userFileRepository)
        {
            _userFileRepository = userFileRepository;
        }

        [HttpPost]
        [Route("api/userFile")]
        public async Task<ActionResult<UserFile>> PostUserFile([FromBody] UserFileDTO userFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "The request contains invalid data.",
                    Error = true,
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }
            try
            {
                var (newUserFile, message, statusCode) = await _userFileRepository.Add(userFile);
                return Created("",new
                {
                    Status = statusCode,
                    Message = message,
                    NewUserFile = newUserFile,
                    Error = false
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Error when creating the userFile.",
                    Error = true,
                    ErrorMessage = ex.Message
                });
            }
        }

    }
}