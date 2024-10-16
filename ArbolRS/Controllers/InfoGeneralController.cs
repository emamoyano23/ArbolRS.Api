using ArbolRS.Code;
using ArbolRS.Data;
using ArbolRS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class InfoGeneralController : ArbolControllerBase
    {
        private readonly IInfoGeneral _ServicioApi;
        public InfoGeneralController(IInfoGeneral servicio)
        {
            _ServicioApi = servicio;
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<List<InfoGeneral>> Search(string nombre)
        {
            var altura = await _ServicioApi.LstInfoGeneral(nombre);
            return altura;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<InfoGeneral> TakeId(int id)
        {
            var altura = await _ServicioApi.GetInfoGeneral(id);
            return altura;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(InfoGeneral modelo)
        {
            var altura = await _ServicioApi.Create(modelo);
            return Ok();
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(InfoGeneral modelo)
        {
            var altura = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(InfoGeneral modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
}
