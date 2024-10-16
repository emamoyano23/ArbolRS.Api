using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolRS.Entity;
using Dapper;

namespace ArbolRS.Data
{

    public interface IArbol
    {
        Task<int> Create(Arbol arbol);
        Task<int> Edit(Arbol arbol);
        Task Disable(Arbol arbol);
        Task<Arbol> GetArbol(int id);
        Task<List<Arbol>> LstArbol(string search);
    }

    public class ArbolServices : IArbol
    {
        public ArbolServices()
        {

        }
        public async Task<List<Arbol>> LstArbol(string search)
        {
            try
            {

                using (var sql = Connect.Sql())
                {
                    if (string.IsNullOrWhiteSpace(search))

                        return (await sql.QueryAsync <Arbol> ("select* from Arbol where activo = 1 ")).ToList();
                    else
                        return (await sql.QueryAsync <Arbol> (@"select* from Users whereFoto active = 1 and like '%' + @search + '%' ", new { search })).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<Arbol> GetArbol(int Id)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    return await sql.QueryFirstAsync <Arbol> ("select* from Arbol where Id = @Id", new { Id });
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<int> Create(Arbol arbol)
        {
            int id = 0;
            try
            {
                using (var sql = Connect.Sql())
                {
                    id = await sql.QuerySingleAsync<int>(@"insert into Arbol values (@NombreArbolId, @AlturaId, @AnchoId, @EdadId, @Foto, @ArbolHistorico, @InfoGeneralId, @activo);select scope_identity();", arbol);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return id;
        }
        public async Task Disable(Arbol arbol)
        {
            try
            {
                arbol.activo = !arbol.activo;
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update Arbol set activo = @activo where Id = @Id", arbol);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
        }
        public async Task<int> Edit(Arbol arbol)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update Arbol set NombreArbolId = @NombreArbolId, AlturaId = @AlturaId, AnchoId = @AnchoId, EdadId = @EdadId, Foto = @Foto, ArbolHistorico = @ArbolHistorico, InfoGeneralId = @InfoGeneralId, activo = @activo where Id = @Id", arbol);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return arbol.Id;
        }
    }
}
