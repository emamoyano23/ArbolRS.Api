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

    public interface IAncho
    {
        Task<int> Create(Ancho ancho);
        Task<int> Edit(Ancho ancho);
        Task Disable(Ancho ancho);
        Task<Ancho> GetAncho(int id);
        Task<List<Ancho>> LstAncho(string search);
    }

    public class AnchoServices : IAncho
    {
        public AnchoServices()
        {

        }
        public async Task<List<Ancho>> LstAncho(string search)
        {
            try
            {

                using (var sql = Connect.Sql())
                {
                    if (string.IsNullOrWhiteSpace(search))

                        return (await sql.QueryAsync<Ancho>("select* from Ancho where activo = 1 ")).ToList();
                    else
                        return (await sql.QueryAsync<Ancho>(@"select* from Users whereAnchoArbol active = 1 and like '%' + @search + '%' ", new { search })).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<Ancho> GetAncho(int Id)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    return await sql.QueryFirstAsync<Ancho>("select* from Ancho where Id = @Id", new { Id });
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<int> Create(Ancho ancho)
        {
            int id = 0;
            try
            {
                using (var sql = Connect.Sql())
                {
                    id = await sql.QuerySingleAsync<int>(@"insert into Ancho values (@AnchoArbol, @activo);select scope_identity();", ancho);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return id;
        }
        public async Task Disable(Ancho ancho)
        {
            try
            {
                ancho.activo = !ancho.activo;
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update Ancho set activo = @activo where Id = @Id", ancho);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
        }
        public async Task<int> Edit(Ancho ancho)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update Ancho set AnchoArbol = @AnchoArbol, activo = @activo where Id = @Id", ancho);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return ancho.Id;
        }
    }
}
