using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniDrive.Services;
using MiniDrive.Models;
//using MiniDrive.DTOs;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<Folder>>> GetAll()
        {
            try
            {
                var (folders, message, statusCode) = await _folderRepository.GetAll();
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
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var (folder, message, statusCode) = await _folderRepository.GetById(id);
                if (folder == null)
                {
                    return NotFound(new
                    {
                        status = StatusCodes.Status404NotFound,
                        message = $"Folder not found: {id}",
                        error = true
                    });
                }

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Folder found",
                    folder,
                    error = false
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = StatusCodes.Status500InternalServerError,
                    message = "Internal Server Error",
                    error = true,
                    errorMessage = ex.Message
                });
            }
        }
    }
}