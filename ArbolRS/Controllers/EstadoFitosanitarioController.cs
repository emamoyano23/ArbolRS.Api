using ArbolRS.Code;
using ArbolRS.Data;
using ArbolRS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class EstadoFitosanitarioController : ArbolControllerBase
    {
        private readonly IEstadoFitosanitario _ServicioApi;
        public EstadoFitosanitarioController(IEstadoFitosanitario servicioApi)
        {
            _ServicioApi = servicioApi;
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<List<EstadoFitosanitario>> Search(string nombre)
        {
            var altura = await _ServicioApi.LstEstadoFitosanitario(nombre);
            return altura;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<EstadoFitosanitario> TakeId(int id)
        {
            var altura = await _ServicioApi.GetEstadoFitosanitario(id);
            return altura;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(EstadoFitosanitario modelo)
        {
            var altura = await _ServicioApi.Create(modelo);
            return Ok();
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(EstadoFitosanitario modelo)
        {
            var altura = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(EstadoFitosanitario modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
}
