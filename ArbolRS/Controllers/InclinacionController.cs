using ArbolRS.Code;
using ArbolRS.Data;
using ArbolRS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class InclinacionController : ArbolControllerBase
    {
        private readonly IInclinacion _ServicioApi;
        public InclinacionController(IInclinacion servicio)
        {
            _ServicioApi = servicio;
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<List<Inclinacion>> Search(string nombre)
        {
            var altura = await _ServicioApi.LstInclinacion(nombre);
            return altura;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<Inclinacion> TakeId(int id)
        {
            var altura = await _ServicioApi.GetInclinacion(id);
            return altura;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(Inclinacion modelo)
        {
            var altura = await _ServicioApi.Create(modelo);
            return Ok();
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(Inclinacion modelo)
        {
            var altura = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(Inclinacion modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
}
