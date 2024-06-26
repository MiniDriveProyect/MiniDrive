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

namespace MiniDrive.Controllers.Folders
{
    [Authorize]
    public class FolderUpdateController : ControllerBase
    {
        private readonly IFolderRepository _folderRepository;

        public FolderUpdateController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        [HttpPost]
        [Route("api/folder")]
        public async Task<IActionResult> UpdateFolder(int id, [FromBody] FolderDTO folderDTO)
        {
            try
            {
                var (folder, message, statusCode) = await _folderRepository.Update(id, folderDTO);
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
                    message = "Carpeta actualizada con éxito",
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