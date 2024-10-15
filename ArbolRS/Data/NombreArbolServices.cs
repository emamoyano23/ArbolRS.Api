using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolRS;
using ArbolRS.Entity;
using Dapper;

namespace ArbolRSData
{
    public interface INombreArbol
    {
        Task<int> Create(NombreArbol nombrearbol);
        Task<int> Edit(NombreArbol nombrearbol);
        Task Disable(NombreArbol nombrearbol);
        Task<NombreArbol> GetNombreArbol(int id);
        Task<List<NombreArbol>> LstNombreArbol(string search);
    }

    public class NombreArbolServices : INombreArbol
    {
        public NombreArbolServices()
        {

        }
        public async Task<List<NombreArbol>> LstNombreArbol(string search)
        {
            try
            {

                using (var sql = Connect.Sql())
                {
                    if (string.IsNullOrWhiteSpace(search))

                        return (await sql.QueryAsync<NombreArbol>("select * from NombreArbol where activo = 1 order by nombre")).ToList();
                    else
                        return (await sql.QueryAsync<NombreArbol>(@"select* from NombreArbol where nombre active = 1 and like '%' + @search + '%'  or nombre active = 1 and like '%' + @search + '%' ", new { search })).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<NombreArbol> GetNombreArbol(int Id)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    return await sql.QueryFirstAsync<NombreArbol>("select* from NombreArbol where Id = @Id", new { Id });
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<int> Create(NombreArbol nombrearbol)
        {
            int id = 0;
            try
            {
                using (var sql = Connect.Sql())
                {
                    id = await sql.QuerySingleAsync<int>(@"insert into NombreArbol values (@nombreCientifico, @nombre, @activo);select scope_identity();", nombrearbol);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return id;
        }
        public async Task Disable(NombreArbol nombrearbol)
        {
            try
            {
                nombrearbol.activo = !nombrearbol.activo;
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update NombreArbol set activo = @activo where Id = @Id", nombrearbol);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
        }
        public async Task<int> Edit(NombreArbol nombrearbol)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update NombreArbol set nombreCientifico = @nombreCientifico, nombre = @nombre, activo = @activo where Id = @Id", nombrearbol);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return nombrearbol.Id;
        }
    }
}
