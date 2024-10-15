using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolRS.Entity;
using Dapper;

namespace ArbolRS.Data;


public interface ITipoLugar
{
    Task<int> Create(TipoLugar tipolugar);
    Task<int> Edit(TipoLugar tipolugar);
    Task Disable(TipoLugar tipolugar);
    Task<TipoLugar> GetTipoLugar(int id);
    Task<List<TipoLugar>> LstTipoLugar(string search);
}

public class TipoLugarServices : ITipoLugar
{
    public TipoLugarServices()
    {

    }
    public async Task<List<TipoLugar>> LstTipoLugar(string search)
    {
        try
        {

            using (var sql = Connect.Sql())
            {
                if (string.IsNullOrWhiteSpace(search))

                    return (await sql.QueryAsync<TipoLugar>("select* from TipoLugar where activo = 1 ")).ToList();
                else
                    return (await sql.QueryAsync<TipoLugar>(@"select* from Users whereTipoLugarArbol active = 1 and like '%' + @search + '%' ", new { search })).ToList();
            }
        }
        catch (Exception)
        {
            throw new Exception("Error al consultar información en DB");
        }
    }
    public async Task<TipoLugar> GetTipoLugar(int Id)
    {
        try
        {
            using (var sql = Connect.Sql())
            {
                return await sql.QueryFirstAsync<TipoLugar>("select* from TipoLugar where Id = @Id", new { Id });
            }
        }
        catch (Exception)
        {
            throw new Exception("Error al consultar información en DB");
        }
    }
    public async Task<int> Create(TipoLugar tipolugar)
    {
        int id = 0;
        try
        {
            using (var sql = Connect.Sql())
            {
                id = await sql.QuerySingleAsync<int>(@"insert into TipoLugar values (@TipoLugarArbol, @activo);select scope_identity();", tipolugar);
            }
        }
        catch (Exception)
        {
            throw new Exception("Error al guardad información en DB");
        }
        return id;
    }
    public async Task Disable(TipoLugar tipolugar)
    {
        try
        {
            tipolugar.activo = !tipolugar.activo;
            using (var sql = Connect.Sql())
            {
                await sql.ExecuteAsync(@"update TipoLugar set activo = @activo where Id = @Id", tipolugar);
            }
        }
        catch (Exception)
        {
            throw new Exception("Error al guardad información en DB");
        }
    }
    public async Task<int> Edit(TipoLugar tipolugar)
    {
        try
        {
            using (var sql = Connect.Sql())
            {
                await sql.ExecuteAsync(@"update TipoLugar set TipoLugarArbol = @TipoLugarArbol, activo = @activo where Id = @Id", tipolugar);
            }
        }
        catch (Exception)
        {
            throw new Exception("Error al guardad información en DB");
        }
        return tipolugar.Id;
    }
}
