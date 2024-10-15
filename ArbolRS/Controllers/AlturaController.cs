using ArbolRS.Code;
using ArbolRS.Data;
using ArbolRS.Entity;
using ArbolRSData;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class AlturaController : ArbolControllerBase
    {
        private readonly IAltura _ServicioApi;
        public AlturaController(IAltura servicio)
        {
            _ServicioApi = servicio;
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<List<Altura>> Search(string nombre)
        {
            var altura = await _ServicioApi.LstAltura(nombre);
            return altura;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<Altura> TakeId(int id)
        {
            var altura = await _ServicioApi.GetAltura(id);
            return altura;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(Altura modelo)
        {
            var altura = await _ServicioApi.Create(modelo);
            return Ok();
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(Altura modelo)
        {
            var altura = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(Altura modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
}
