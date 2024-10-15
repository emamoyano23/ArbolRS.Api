using ArbolRS.Code;
using ArbolRS.Data;
using ArbolRS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class AnchoController : ArbolControllerBase
    {
        private readonly IAncho _ServicioApi;

        public AnchoController(IAncho servicio)
        {
            _ServicioApi = servicio;
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<List<Ancho>> Search(string nombre)
        {
            var ancho = await _ServicioApi.LstAncho(nombre);
        return ancho;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<Ancho> TakeId(int id)
        {
            var ancho = await _ServicioApi.GetAncho(id);
            return ancho;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(Ancho modelo)
        {
            var ancho = await _ServicioApi.Create(modelo);
            return Ok();
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(Ancho modelo)
        {
            var ancho = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(Ancho modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
   
}
