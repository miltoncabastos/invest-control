using InvestControl.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestControl.API.Controllers
{
    [ApiController]
    [Route("api/uploadInformations")]
    public class UploadInformationsController : ControllerBase
    {
        [HttpGet("/upload-data-manual")]
        public IActionResult StartUploadInformations([FromServices] IUploadInformationsService uploadInformationsService)
        {
            uploadInformationsService.StartUploadInformation();
            return Ok("Informações importadas com sucesso.");
        }
        
        [HttpGet("/upload-for-export-b3-movimentacoes")]
        public IActionResult StartUploadB3Movimentacoes([FromServices] IUploadB3MovimentacaoService uploadB3MovimentacaoService)
        {
            uploadB3MovimentacaoService.StartUpload();
            return Ok("Informações da b3 importadas com sucesso.");
        }
    }
}