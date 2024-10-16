using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolRS.Entity;
using Dapper;

namespace ArbolRS.Data
{

    public interface IInfoAvanzada
    {
        Task<int> Create(InfoAvanzada infoavanzada);
        Task<int> Edit(InfoAvanzada infoavanzada);
        Task Disable(InfoAvanzada infoavanzada);
        Task<InfoAvanzada> GetInfoAvanzada(int id);
        Task<List<InfoAvanzada>> LstInfoAvanzada(string search);
    }

    public class InfoAvanzadaServices : IInfoAvanzada
    {
        public InfoAvanzadaServices()
        {

        }
        public async Task<List<InfoAvanzada>> LstInfoAvanzada(string search)
        {
            try
            {

                using (var sql = Connect.Sql())
                {
                    if (string.IsNullOrWhiteSpace(search))

                        return (await sql.QueryAsync <InfoAvanzada> ("select* from InfoAvanzada where activo = 1 ")).ToList();
                    else
                        return (await sql.QueryAsync <InfoAvanzada> (@"select* from Users where", new { search })).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<InfoAvanzada> GetInfoAvanzada(int Id)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    return await sql.QueryFirstAsync <InfoAvanzada> ("select* from InfoAvanzada where Id = @Id", new { Id });
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<int> Create(InfoAvanzada infoavanzada)
        {
            int id = 0;
            try
            {
                using (var sql = Connect.Sql())
                {
                    id = await sql.QuerySingleAsync<int>(@"insert into InfoAvanzada values (@ArbolId, @FaseVitalId, @EstadoFitosanitarioId, @InclinacionId, @CazuelaId, @activo);select scope_identity();", infoavanzada);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return id;
        }
        public async Task Disable(InfoAvanzada infoavanzada)
        {
            try
            {
                infoavanzada.activo = !infoavanzada.activo;
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update InfoAvanzada set activo = @activo where Id = @Id", infoavanzada);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
        }
        public async Task<int> Edit(InfoAvanzada infoavanzada)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update InfoAvanzada set ArbolId = @ArbolId, FaseVitalId = @FaseVitalId, EstadoFitosanitarioId = @EstadoFitosanitarioId, InclinacionId = @InclinacionId, CazuelaId = @CazuelaId, activo = @activo where Id = @Id", infoavanzada);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return infoavanzada.Id;
        }
    }
}
