using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniDrive.Services;
using MiniDrive.Models;
//using MiniDrive.DTOs;
using System.Threading.Tasks;
using MiniDrive.Services.Interfaces;


namespace MiniDrive.Controllers.UserFiles
{
    //[Authorize]
    public class UserFilesController : ControllerBase
    {
        private readonly IUserFileRepository _userfileRepository;

        public UserFilesController(IUserFileRepository userfileRepository)
        {
            _userfileRepository = userfileRepository;
        }

        [HttpGet]
        [Route("/api/userfiles")]
        public async Task<ActionResult<IEnumerable<UserFile>>> GetAll()
        {
            var marketing = await _userfileRepository.GetAll();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Usuarios listados exitosamente",
                marketing
            });
        }

        [HttpGet]
        [Route("api/userfiles/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
        try
        {
            var (userFile, message, statusCode) = await _userfileRepository.GetById(id);
            if (userFile == null)
            {
                return StatusCode((int)statusCode, new
                {
                    status = statusCode,
                    message,
                    error = true
                });
            }

            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Archivo encontrado exitosamente",
                error = false,
                userFile
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