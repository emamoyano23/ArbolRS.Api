using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolRS.Entity;
using Dapper;

namespace ArbolRS.Data
{

    public interface IEdad
    {
        Task<int> Create(Edad edad);
        Task<int> Edit(Edad edad);
        Task Disable(Edad edad);
        Task<Edad> GetEdad(int id);
        Task<List<Edad>> LstEdad(string search);
    }

    public class EdadServices : IEdad
    {
        public EdadServices()
        {

        }
        public async Task<List<Edad>> LstEdad(string search)
        {
            try
            {

                using (var sql = Connect.Sql())
                {
                    if (string.IsNullOrWhiteSpace(search))

                        return (await sql.QueryAsync<Edad>("select* from Edad where activo = 1 ")).ToList();
                    else
                        return (await sql.QueryAsync<Edad>(@"select* from Users whereEdadArbol active = 1 and like '%' + @search + '%' ", new { search })).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<Edad> GetEdad(int Id)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    return await sql.QueryFirstAsync <Edad> ("select* from Edad where Id = @Id", new { Id });
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<int> Create(Edad edad)
        {
            int id = 0;
            try
            {
                using (var sql = Connect.Sql())
                {
                    id = await sql.QuerySingleAsync<int>(@"insert into Edad values (@EdadArbol, @activo);select scope_identity();", edad);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return id;
        }
        public async Task Disable(Edad edad)
        {
            try
            {
                edad.activo = !edad.activo;
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update Edad set activo = @activo where Id = @Id", edad);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
        }
        public async Task<int> Edit(Edad edad)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update Edad set EdadArbol = @EdadArbol, activo = @activo where Id = @Id", edad);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return edad.Id;
        }
    }
}
