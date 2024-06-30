using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniDrive.Services;
using MiniDrive.Models;
using MiniDrive.Services.Interfaces;


namespace MiniDrive.Controllers.Folders
{
    //[Authorize]
    public class FoldersController : ControllerBase
    {
        private readonly IFolderRepository _folderRepository;

        public FoldersController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        [HttpGet]
        [Route("/api/folders")]
        public async Task<ActionResult<IEnumerable<Folder>>> GetAll(int userId)
        {
            try
            {
                var (folders, message, statusCode) = await _folderRepository.GetAll(userId);
                if (folders == null || folders == Enumerable.Empty<Folder>())
                {
                    return NotFound(message);
                }

                return Ok(new
                {
                    Status = statusCode,
                    Message = message,
                    Folders = folders
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error obtaining folders: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("api/folders/{id}")]
        public async Task<IActionResult> GetById(int id, int userId)
        {
            try
            {
                var (folder, message, statusCode) = await _folderRepository.GetById(id, userId);
                if (folder == null)
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
                    Folder = folder,
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