using ArbolRS.Code;
using ArbolRS.Data;
using ArbolRS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class TipoRelevamientoController : ArbolControllerBase
    {
        private readonly ITipoRelevamiento _ServicioApi;
        public TipoRelevamientoController(ITipoRelevamiento servicioApi)
        {
            _ServicioApi = servicioApi;
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<List<TipoRelevamiento>> Search(string nombre)
        {
            var altura = await _ServicioApi.LstTipoRelevamiento(nombre);
            return altura;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<TipoRelevamiento> TakeId(int id)
        {
            var altura = await _ServicioApi.GetTipoRelevamiento(id);
            return altura;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(TipoRelevamiento modelo)
        {
            var altura = await _ServicioApi.Create(modelo);
            return Ok();
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(TipoRelevamiento modelo)
        {
            var altura = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(TipoRelevamiento modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
}
