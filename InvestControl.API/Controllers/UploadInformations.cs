using InvestControl.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestControl.API.Controllers
{
    [ApiController]
    [Route("api/uploadInformations")]
    public class UploadInformations : ControllerBase
    {
        [HttpGet]
        public IActionResult StartUploadInformations([FromServices] IUploadInformationsService uploadInformationsService)
        {
            uploadInformationsService.StartUploadInformation();
            return Ok("Informações importadas com sucesso.");
        }
        
    }
}