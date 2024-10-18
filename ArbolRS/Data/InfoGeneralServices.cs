using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolRS.Entity;
using Dapper;

namespace ArbolRS.Data
{

    public interface IInfoGeneral
    {
        Task<int> Create(InfoGeneral infogeneral);
        Task<int> Edit(InfoGeneral infogeneral);
        Task Disable(InfoGeneral infogeneral);
        Task<InfoGeneral> GetInfoGeneral(int id);
        Task<List<InfoGeneral>> LstInfoGeneral(string search);
    }

    public class InfoGeneralServices : IInfoGeneral
    {
        public InfoGeneralServices()
        {

        }
        public async Task<List<InfoGeneral>> LstInfoGeneral(string search)
        {
            try
            {

                using (var sql = Connect.Sql())
                {
                    if (string.IsNullOrWhiteSpace(search))

                        return (await sql.QueryAsync <InfoGeneral> ("select* from InfoGeneral where activo = 1 ")).ToList();
                    else
                        return (await sql.QueryAsync <InfoGeneral> (@"select* from Users wherenombre active = 1 and like '%' + @search + '%' ", new { search })).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<InfoGeneral> GetInfoGeneral(int Id)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    return await sql.QueryFirstAsync <InfoGeneral> ("select* from InfoGeneral where Id = @Id", new { Id });
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<int> Create(InfoGeneral infogeneral)
        {
            int id = 0;
            try
            {
                using (var sql = Connect.Sql())
                {
                    id = await sql.QuerySingleAsync<int>(@"insert into InfoGeneral values (@UserId, @tipoRelevamientoId, @nombre, @tipoLugarId, @DireccionId, @AlturaDireccion, @retiroVerde, @arbolId, @activo);select scope_identity();", infogeneral);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return id;
        }
        public async Task Disable(InfoGeneral infogeneral)
        {
            try
            {
                infogeneral.activo = !infogeneral.activo;
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update InfoGeneral set activo = @activo where Id = @Id", infogeneral);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
        }
        public async Task<int> Edit(InfoGeneral infogeneral)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update InfoGeneral set UserId = @UserId, tipoRelevamientoId = @tipoRelevamientoId, nombre = @nombre, tipoLugarId = @tipoLugarId, DireccionId = @DireccionId, AlturaDireccion = @AlturaDireccion, retiroVerde = @retiroVerde, arbolId = @arbolId, activo = @activo where Id = @Id", infogeneral);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return infogeneral.Id;
        }
    }
}
