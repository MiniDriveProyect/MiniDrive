using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniDrive.DTOs;
using MiniDrive.Models;
using MiniDrive.Services.Interfaces;

namespace MiniDrive.Controllers.Folders
{
    public class FolderCreateController : ControllerBase
    {
        private readonly IFolderRepository _folderRepository;

        public FolderCreateController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        [HttpPost]
        [Route("api/folder")]
        public async Task<ActionResult<Folder>> PostFolder( [FromBody] FolderDTO folder)
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
                await _folderRepository.Add(folder);
                return Created("", new
                {
                    status = StatusCodes.Status201Created,
                    message = "Usuario de marketing creado exitosamente",
                    error = false
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = StatusCodes.Status500InternalServerError,
                    message = "Error al crear el historial.",
                    error = true,
                    errorMessage = ex.Message
                });
            }
        }

    }
}