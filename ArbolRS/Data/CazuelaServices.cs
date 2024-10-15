using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolRS.Entity;
using Dapper;

namespace ArbolRS.Data
{
    public interface ICazuela
    {
        Task<int> Create(Cazuela cazuela);
        Task<int> Edit(Cazuela cazuela);
        Task Disable(Cazuela cazuela);
        Task<Cazuela> GetCazuela(int id);
        Task<List<Cazuela>> LstCazuela(string search);
    }

    public class CazuelaServices : ICazuela
    {
        public CazuelaServices()
        {

        }
        public async Task<List<Cazuela>> LstCazuela(string search)
        {
            try
            {

                using (var sql = Connect.Sql())
                {
                    if (string.IsNullOrWhiteSpace(search))

                        return (await sql.QueryAsync<Cazuela>("select* from Cazuela where activo = 1 ")).ToList();
                    else
                        return (await sql.QueryAsync<Cazuela>(@"select* from Users wherecazuelaArbol active = 1 and like '%' + @search + '%' ", new { search })).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<Cazuela> GetCazuela(int Id)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    return await sql.QueryFirstAsync<Cazuela>("select* from Cazuela where Id = @Id", new { Id });
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<int> Create(Cazuela cazuela)
        {
            int id = 0;
            try
            {
                using (var sql = Connect.Sql())
                {
                    id = await sql.QuerySingleAsync<int>(@"insert into Cazuela values (@cazuelaArbol, @activo);select scope_identity();", cazuela);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return id;
        }
        public async Task Disable(Cazuela cazuela)
        {
            try
            {
                cazuela.activo = !cazuela.activo;
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update Cazuela set activo = @activo where Id = @Id", cazuela);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
        }
        public async Task<int> Edit(Cazuela cazuela)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update Cazuela set cazuelaArbol = @cazuelaArbol, activo = @activo where Id = @Id", cazuela);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return cazuela.Id;
        }
    }
}
