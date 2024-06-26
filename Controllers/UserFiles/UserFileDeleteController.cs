using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniDrive.Services.Interfaces;

namespace MiniDrive.Controllers.UserFiles
{
    [Authorize]
    public class UserFileDeleteController : ControllerBase
    {
        private readonly IUserFileRepository _userfileRepository;

        public UserFileDeleteController(IUserFileRepository userfileRepository)
        {
            _userfileRepository = userfileRepository;
        }

        [HttpDelete]
        [Route("api/userfile/{id}")]
        public async Task<IActionResult> DeleteUserFile(int id)
        {
            try
            {
                var (userFile, message, statusCode) = await _userfileRepository.Delete(id);
                if (userFile == null)
                {
                    return NotFound(new
                    {
                        status = StatusCodes.Status404NotFound,
                        message = "Archivo no encontrado",
                        error = true,
                        errorMessage = "El archivo con el ID proporcionado no existe."
                    });
                }

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "El archivo ha sido eliminado con éxito",
                    error = false,
                    errorMessage = ""
                });
            }
            catch (Exception e)
            {
                // Notificación para el desarrollador (Slack)
                // await _slackNotifier.SendNotify("Error:" + e.Message);

                // Respuesta para el usuario
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = StatusCodes.Status500InternalServerError,
                    message = "Error al tratar de eliminar el archivo.",
                    error = true,
                    errorMessage = e.Message
                });
            }
        }
    }
}