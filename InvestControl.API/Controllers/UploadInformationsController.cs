using InvestControl.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestControl.API.Controllers
{
    [ApiController]
    [Route("api/upload-informations")]
    public class UploadInformationsController : ControllerBase
    {
        [HttpGet]
        [Route("upload-data-manual")]
        public IActionResult StartUploadInformations([FromServices] IUploadInformationsService uploadInformationsService)
        {
            uploadInformationsService.StartUploadInformation();
            return Ok("Informações importadas com sucesso.");
        }
        
        [HttpGet]
        [Route("upload-for-export-b3-movimentacoes")]
        public IActionResult StartUploadB3Movimentacoes([FromServices] IUploadB3MovimentacaoService uploadB3MovimentacaoService)
        {
            uploadB3MovimentacaoService.StartUpload();
            return Ok("Informações da b3 importadas com sucesso.");
        }
    }
}