using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolRS.Entity;
using Dapper;

namespace ArbolRS.Data
{

    public interface ITipoRelevamiento
    {
        Task<int> Create(TipoRelevamiento tiporelevamiento);
        Task<int> Edit(TipoRelevamiento tiporelevamiento);
        Task Disable(TipoRelevamiento tiporelevamiento);
        Task<TipoRelevamiento> GetTipoRelevamiento(int id);
        Task<List<TipoRelevamiento>> LstTipoRelevamiento(string search);
    }

    public class TipoRelevamientoServices : ITipoRelevamiento
    {
        public TipoRelevamientoServices()
        {

        }
        public async Task<List<TipoRelevamiento>> LstTipoRelevamiento(string search)
        {
            try
            {

                using (var sql = Connect.Sql())
                {
                    if (string.IsNullOrWhiteSpace(search))

                        return (await sql.QueryAsync <TipoRelevamiento> ("select* from TipoRelevamiento where activo = 1 ")).ToList();
                    else
                        return (await sql.QueryAsync <TipoRelevamiento> (@"select* from Users whereTipoRelevamientoArbol active = 1 and like '%' + @search + '%' ", new { search })).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<TipoRelevamiento> GetTipoRelevamiento(int Id)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    return await sql.QueryFirstAsync<TipoRelevamiento>("select* from TipoRelevamiento where Id = @Id", new { Id });
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<int> Create(TipoRelevamiento tiporelevamiento)
        {
            int id = 0;
            try
            {
                using (var sql = Connect.Sql())
                {
                    id = await sql.QuerySingleAsync<int>(@"insert into TipoRelevamiento values (@TipoRelevamientoArbol, @activo);select scope_identity();", tiporelevamiento);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return id;
        }
        public async Task Disable(TipoRelevamiento tiporelevamiento)
        {
            try
            {
                tiporelevamiento.activo = !tiporelevamiento.activo;
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update TipoRelevamiento set activo = @activo where Id = @Id", tiporelevamiento);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
        }
        public async Task<int> Edit(TipoRelevamiento tiporelevamiento)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update TipoRelevamiento set TipoRelevamientoArbol = @TipoRelevamientoArbol, activo = @activo where Id = @Id", tiporelevamiento);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return tiporelevamiento.Id;
        }
    }
}
