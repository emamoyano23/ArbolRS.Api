using ArbolRS.Data;
using ArbolRS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class ArbolController : Controller
    {
        private readonly IArbol _ServicioApi;
        public ArbolController(IArbol servicio)
        {
            _ServicioApi = servicio;
        }

        [HttpGet]
        [Route("buscar")]
        public async Task<List<Arbol>> Search(string nombre)
        {
            var altura = await _ServicioApi.LstArbol(nombre);
            return altura;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<Arbol> TakeId(int id)
        {
            var altura = await _ServicioApi.GetArbol(id);
            return altura;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(Arbol modelo)
        {
            var altura = await _ServicioApi.Create(modelo);
            return Ok();
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(Arbol modelo)
        {
            var altura = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(Arbol modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
}
