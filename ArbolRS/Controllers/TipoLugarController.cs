using ArbolRS.Code;
using ArbolRS.Data;
using ArbolRS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ArbolRS.Controllers
{
    public class TipoLugarController : ArbolControllerBase
    {
        private readonly ITipoLugar _ServicioApi;
        public TipoLugarController(ITipoLugar servicio)
        {
            _ServicioApi = servicio;
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<List<TipoLugar>> Search(string nombre)
        {
            var altura = await _ServicioApi.LstTipoLugar(nombre);
            return altura;
        }
        [HttpGet]
        [Route("id/{id:int}")]
        public async Task<TipoLugar> TakeId(int id)
        {
            var altura = await _ServicioApi.GetTipoLugar(id);
            if (altura == null)
            {
                return new TipoLugar { TipoLugarArbol = "No se encontró resultado." };
            }
            return altura;
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Create(TipoLugar modelo)
        {
            var existe = _ServicioApi.LstTipoLugar(modelo.TipoLugarArbol);
            if (existe != null)
            {
                return NotFound($"Este dato ya existe {modelo.TipoLugarArbol}");
            }
            var altura = await _ServicioApi.Create(modelo);
            return Ok();
        }
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> Edit(TipoLugar modelo)
        {
            var altura = await _ServicioApi.Edit(modelo);
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult> Disable(TipoLugar modelo)
        {
            await _ServicioApi.Disable(modelo);
            return Ok();
        }
    }
}
