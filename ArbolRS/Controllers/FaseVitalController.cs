using ArbolRS.Code;
using ArbolRS.Data;
using ArbolRS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class FaseVitalController : ArbolControllerBase
    {
        private readonly IFaseVital _ServicioApi;
        public FaseVitalController(IFaseVital servicio)
        {
            _ServicioApi = servicio;
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<List<FaseVital>> Search(string nombre)
        {
            var altura = await _ServicioApi.LstFaseVital(nombre);
            return altura;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<FaseVital> TakeId(int id)
        {
            var altura = await _ServicioApi.GetFaseVital(id);
            return altura;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(FaseVital modelo)
        {
            var altura = await _ServicioApi.Create(modelo);
            return Ok();
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(FaseVital modelo)
        {
            var altura = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(FaseVital modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
}
