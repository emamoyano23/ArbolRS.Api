using ArbolRS.Code;
using ArbolRS.Data;
using ArbolRS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class EdadController : ArbolControllerBase
    {
        private readonly IEdad _ServicioApi;
        public EdadController(IEdad servicio)
        {
            _ServicioApi = servicio;
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<List<Edad>> Search(string nombre)
        {
            var altura = await _ServicioApi.LstEdad(nombre);
            return altura;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<Edad> TakeId(int id)
        {
            var altura = await _ServicioApi.GetEdad(id);
            return altura;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(Edad modelo)
        {
            var altura = await _ServicioApi.Create(modelo);
            return Ok(altura);
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(Edad modelo)
        {
            var altura = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(Edad modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
}
