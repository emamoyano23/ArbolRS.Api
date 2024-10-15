using ArbolRS.Code;
using ArbolRS.Entity;
using ArbolRSData;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class NombreArbolController : ArbolControllerBase
    {
        private readonly INombreArbol _ServicioApi;
        public NombreArbolController(INombreArbol servicio)
        {
            _ServicioApi = servicio;
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<List<NombreArbol>> Search(string nombre)
        {
            var nombreArbol = await _ServicioApi.LstNombreArbol(nombre);
            return nombreArbol;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<NombreArbol> TakeId(int id)
        {
            var nombreArbol = await _ServicioApi.GetNombreArbol(id);
            return nombreArbol;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(NombreArbol modelo)
        {
            var nombreArbol = await _ServicioApi.Create(modelo);
            return Ok();
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(NombreArbol modelo)
        {
            var nombreArbol = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(NombreArbol modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
}
