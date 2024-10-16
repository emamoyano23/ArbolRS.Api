using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolRS.Entity;
using Dapper;

namespace ArbolRS.Data
{

    public interface IFaseVital
    {
        Task<int> Create(FaseVital fasevital);
        Task<int> Edit(FaseVital fasevital);
        Task Disable(FaseVital fasevital);
        Task<FaseVital> GetFaseVital(int id);
        Task<List<FaseVital>> LstFaseVital(string search);
    }

    public class FaseVitalServices : IFaseVital
    {
        public FaseVitalServices()
        {

        }
        public async Task<List<FaseVital>> LstFaseVital(string search)
        {
            try
            {

                using (var sql = Connect.Sql())
                {
                    if (string.IsNullOrWhiteSpace(search))

                        return (await sql.QueryAsync <FaseVital> ("select* from FaseVital where activo = 1 ")).ToList();
                    else
                        return (await sql.QueryAsync <FaseVital> (@"select* from Users whereFaseVitalArbol active = 1 and like '%' + @search + '%' ", new { search })).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<FaseVital> GetFaseVital(int Id)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    return await sql.QueryFirstAsync <FaseVital> ("select * from FaseVital where Id = @Id", new { Id });
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<int> Create(FaseVital fasevital)
        {
            int id = 0;
            try
            {
                using (var sql = Connect.Sql())
                {
                    id = await sql.QuerySingleAsync<int>(@"insert into FaseVital values (@FaseVitalArbol, @activo);select scope_identity();", fasevital);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return id;
        }
        public async Task Disable(FaseVital fasevital)
        {
            try
            {
                fasevital.activo = !fasevital.activo;
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update FaseVital set activo = @activo where Id = @Id", fasevital);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
        }
        public async Task<int> Edit(FaseVital fasevital)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update FaseVital set FaseVitalArbol = @FaseVitalArbol, activo = @activo where Id = @Id", fasevital);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return fasevital.Id;
        }
    }
}
