using ArbolRS.Code;
using ArbolRS.Data;
using ArbolRS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class CazuelaController : ArbolControllerBase
    {
        private readonly ICazuela _ServicioApi;
        public CazuelaController(ICazuela servicio)
        {
            _ServicioApi = servicio;
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<List<Cazuela>> Search(string nombre)
        {
            var ancho = await _ServicioApi.LstCazuela(nombre);
            return ancho;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<Cazuela> TakeId(int id)
        {
            var ancho = await _ServicioApi.GetCazuela(id);
            return ancho;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(Cazuela modelo)
        {
            var ancho = await _ServicioApi.Create(modelo);
            return Ok();
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(Cazuela modelo)
        {
            var ancho = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(Cazuela modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
}
