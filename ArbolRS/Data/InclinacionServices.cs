using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolRS.Entity;
using Dapper;

namespace ArbolRS.Data
{

    public interface IInclinacion
    {
        Task<int> Create(Inclinacion inclinacion);
        Task<int> Edit(Inclinacion inclinacion);
        Task Disable(Inclinacion inclinacion);
        Task<Inclinacion> GetInclinacion(int id);
        Task<List<Inclinacion>> LstInclinacion(string search);
    }

    public class InclinacionServices : IInclinacion
    {
        public InclinacionServices()
        {

        }
        public async Task<List<Inclinacion>> LstInclinacion(string search)
        {
            try
            {

                using (var sql = Connect.Sql())
                {
                    if (string.IsNullOrWhiteSpace(search))

                        return (await sql.QueryAsync <Inclinacion> ("select* from Inclinacion where activo = 1 ")).ToList();
                    else
                        return (await sql.QueryAsync <Inclinacion> (@"select* from Users whereinclinacionArbol active = 1 and like '%' + @search + '%' ", new { search })).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<Inclinacion> GetInclinacion(int Id)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    return await sql.QueryFirstAsync <Inclinacion> ("select* from Inclinacion where Id = @Id", new { Id });
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<int> Create(Inclinacion inclinacion)
        {
            int id = 0;
            try
            {
                using (var sql = Connect.Sql())
                {
                    id = await sql.QuerySingleAsync<int>(@"insert into Inclinacion values (@inclinacionArbol, @activo);select scope_identity();", inclinacion);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return id;
        }
        public async Task Disable(Inclinacion inclinacion)
        {
            try
            {
                inclinacion.activo = !inclinacion.activo;
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update Inclinacion set activo = @activo where Id = @Id", inclinacion);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
        }
        public async Task<int> Edit(Inclinacion inclinacion)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update Inclinacion set inclinacionArbol = @inclinacionArbol, activo = @activo where Id = @Id", inclinacion);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return inclinacion.Id;
        }
    }
}
