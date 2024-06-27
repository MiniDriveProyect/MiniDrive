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
        public async Task<ActionResult<Folder>> PostFolder([FromBody] FolderDTO folder)
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
                var (newFolder, message, statusCode) = await _folderRepository.Add(folder);
                return Created("",new
                {
                    Status = statusCode,
                    Message = message,
                    NewFolder = newFolder,
                    Error = false
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Error when creating the folder.",
                    Error = true,
                    ErrorMessage = ex.Message
                });
            }
        }

    }
}