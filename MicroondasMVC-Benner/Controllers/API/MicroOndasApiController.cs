using MicroondasMVC_Benner.Models.API;
using MicroondasMVC_Benner.Models.Microondas;
using MicroondasMVC_Benner.Models.PreAquecimento;
using MicroondasMVC_Benner.Repository;
using MicroondasMVC_Benner.Repository.MicroOndas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroondasMVC_Benner.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MicroOndasApiController : ControllerBase
    {

        private readonly IMicroOndasInterface _microOndasInterface;
        public MicroOndasApiController(IMicroOndasInterface microOndasInterface)
        {
            _microOndasInterface = microOndasInterface;
        }

        [HttpGet("Iniciar")]
        public async Task<IActionResult> Iniciar([FromQuery] int? tempo, [FromQuery] int? potencia)
        {
            ResponseModel<MicroOndasModel> response = new ResponseModel<MicroOndasModel>();
            try
            {
                MicroOndasModel? microOndas = _microOndasInterface.IniciarOuAdicionar(tempo, potencia);
                response.SetSucesso(microOndas, 200);
                return StatusCode(response.Codigo, response);
            } catch(Exception ex)
            {
                HttpContext.Items["ExceptionToLog"] = ex;

                response.SetErro(ex.Message, 500);
                return StatusCode(response.Codigo, response);
            }
        }

        [HttpDelete("Cancelar")]
        public async Task<IActionResult> Cancelar([FromQuery] int tempo)
        {
            ResponseModel<MicroOndasModel> response = new ResponseModel<MicroOndasModel>();
            try
            {
                MicroOndasModel? microOndas = _microOndasInterface.PausarOuCancelar(tempo);
                
                if(microOndas == null)
                {
                    response.SetErro("Erro ao Cancelar ou Pausar", 400);
                    return StatusCode(response.Codigo, response);
                }

                response.SetSucesso(microOndas, 200);
                return StatusCode(response.Codigo, response);
            }
            catch (Exception ex)
            {
                HttpContext.Items["ExceptionToLog"] = ex;

                response.SetErro(ex.Message, 500);
                return StatusCode(response.Codigo, response);
            }
        }

        [HttpGet("IniciarPreAquecimento")]
        public async Task<IActionResult> IniciarPreAquecimento([FromQuery] int IdPreAquecimento)
        {
            ResponseModel<MicroOndasModel> response = new ResponseModel<MicroOndasModel>();
            try
            {
                MicroOndasModel? microOndas = _microOndasInterface.IniciarPreAquecimento(IdPreAquecimento);

                if (microOndas == null)
                {
                    response.SetErro("Erro ao iniciar pre aquecimento, valor não encontrado", 400);
                    return StatusCode(response.Codigo, response);
                }

                response.SetSucesso(microOndas, 200);
                return StatusCode(response.Codigo, response);
            }
            catch (Exception ex)
            {
                HttpContext.Items["ExceptionToLog"] = ex;

                response.SetErro(ex.Message, 500);
                return StatusCode(response.Codigo, response);
            }
        }

        [HttpPost("CadastrarPreAquecimento")]
        public async Task<IActionResult> CadastrarPreAquecimento([FromBody] PreAquecimentoModel model)
        {
            ResponseModel<PreAquecimentoModel> response = new ResponseModel<PreAquecimentoModel>();
            try
            {
                PreAquecimentoModel? preAquecimento = _microOndasInterface.CadastrarPreAquecimento(model);

                if (preAquecimento == null)
                {
                    response.SetErro("Erro ao cadastrar Pre Aquecimento", 400);
                    return StatusCode(response.Codigo, response);
                }

                response.SetSucesso(preAquecimento, 200);
                return StatusCode(response.Codigo, response);
            }
            catch (Exception ex)
            {
                HttpContext.Items["ExceptionToLog"] = ex;

                response.SetErro(ex.Message, 500);
                return StatusCode(response.Codigo, response);
            }
        }
    }
}
