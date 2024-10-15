using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolRS;
using ArbolRS.Entity;
using Dapper;

namespace ArbolRS.Data
{

    public interface IAltura
    {
        Task<int> Create(Altura altura);
        Task<int> Edit(Altura altura);
        Task Disable(Altura altura);
        Task<Altura> GetAltura(int id);
        Task<List<Altura>> LstAltura(string search);
    }

    public class AlturaServices : IAltura
    {
        public AlturaServices()
        {

        }
        public async Task<List<Altura>> LstAltura(string search)
        {
            try
            {

                using (var sql = Connect.Sql())
                {
                    if (string.IsNullOrWhiteSpace(search))

                        return (await sql.QueryAsync<Altura>("select * from Altura where activo = 1 ")).ToList();
                    else
                        return (await sql.QueryAsync<Altura>(@"select * from Altura where AlturaArbol active = 1 and like '%' + @search + '%' ", new { search })).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<Altura> GetAltura(int Id)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    return await sql.QueryFirstAsync<Altura>("select* from Altura where Id = @Id", new { Id });
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<int> Create(Altura altura)
        {
            int id = 0;
            try
            {
                using (var sql = Connect.Sql())
                {
                    id = await sql.QuerySingleAsync<int>(@"insert into Altura values (@AlturaArbol, @activo);select scope_identity();", altura);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return id;
        }
        public async Task Disable(Altura altura)
        {
            try
            {
                altura.activo = !altura.activo;
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update Altura set activo = @activo where Id = @Id", altura);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
        }
        public async Task<int> Edit(Altura altura)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update Altura set AlturaArbol = @AlturaArbol, activo = @activo where Id = @Id", altura);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return altura.Id;
        }
    }
}
