using ArbolRS.Data;
using ArbolRS.Entity;

namespace ArbolRS.Logic
{
    public interface IAlturaLogic
    {
        Task<int> Save(Altura altura);
        Task Disable(Altura altura);
        Task<Altura> GetAltura(int id);
        Task<List<Altura>> LstAltura(string search);
    }
    public class AlturaLogic : IAlturaLogic
    {
        private readonly IAltura _altura;
        AlturaServices altura = new AlturaServices();
        private enum TypeValidation
        {
            create,
            update
        }
        private Type validType { get; set; }

        public AlturaLogic()
        {
            _altura = altura;
        }

        public async Task<int> Save(Altura altura)
        {
            if (altura.Id == 0)
            {
                if (validate(altura, TypeValidation.create))
                {
                    var existe = _altura.LstAltura(altura.AlturaArbol);
                    if (existe == null)
                    {
                        return await _altura.Create(altura);
                    }
                }
            }
            else
            {
                if (validate(altura, TypeValidation.update))
                {
                    return await _altura.Edit(altura);
                }
            }
            return 0;
        }

        public async Task<Altura> GetAltura(int Id)
        {
            return await _altura.GetAltura(Id);
        }

        public async Task<List<Altura>> LstAltura(string search)
        {
            return await _altura.LstAltura(search);
        }

        public async Task Disable(Altura altura)
        {
            var existe = _altura.GetAltura(altura.Id);
            if (existe == null)
            await _altura.Disable(altura);
        }

        private bool validate(Altura altura, TypeValidation valid)
        {
            return true;
        }

    }
}
