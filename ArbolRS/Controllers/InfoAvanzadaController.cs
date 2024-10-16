using ArbolRS.Code;
using ArbolRS.Data;
using ArbolRS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class InfoAvanzadaController : ArbolControllerBase
    {
        private readonly IInfoAvanzada _ServicioApi;
        public InfoAvanzadaController(IInfoAvanzada servicio)
        {
            _ServicioApi = servicio;
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<List<InfoAvanzada>> Search(string nombre)
        {
            var altura = await _ServicioApi.LstInfoAvanzada(nombre);
            return altura;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<InfoAvanzada> TakeId(int id)
        {
            var altura = await _ServicioApi.GetInfoAvanzada(id);
            return altura;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(InfoAvanzada modelo)
        {
            var altura = await _ServicioApi.Create(modelo);
            return Ok();
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(InfoAvanzada modelo)
        {
            var altura = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(InfoAvanzada modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
}
