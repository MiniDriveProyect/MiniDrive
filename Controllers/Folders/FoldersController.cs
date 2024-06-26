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
            var marketing = await _folderRepository.GetAll();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Carpetas listados exitosamente",
                marketing
            });
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
                        message = "Carpeta no encontrada",
                        error = true
                    });
                }

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Carpeta encontrada",
                    folder,
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