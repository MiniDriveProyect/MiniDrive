using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniDrive.Services.Interfaces;

namespace MiniDrive.Controllers.Folders
{
    [Authorize]
    public class FolderDeleteController : ControllerBase
    {
        private readonly IFolderRepository _folderRepository;

        public FolderDeleteController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        [HttpDelete]
        [Route("api/folder/{id}")]
        public async Task<IActionResult> DeleteFolder(int id)
        {
            try
            {
                var (folder, message, statusCode) = await _folderRepository.Delete(id);
                if (folder == null)
                {
                    return NotFound(new
                    {
                        status = StatusCodes.Status404NotFound,
                        message = "Carpeta no encontrada",
                        error = true,
                        errorMessage = "La carpeta con el ID proporcionado no existe."
                    });
                }

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "La carpeta ha sido eliminada con éxito",
                    error = false,
                    errorMessage = ""
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = StatusCodes.Status500InternalServerError,
                    message = "Error al tratar de eliminar la carpeta.",
                    error = true,
                    errorMessage = e.Message
                });
            }
        }
    }

}