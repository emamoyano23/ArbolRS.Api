using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolRS.Entity;
using Dapper;

namespace ArbolRS.Data
{
    public interface IEstadoFitosanitario
    {
        Task<int> Create(EstadoFitosanitario estadofitosanitario);
        Task<int> Edit(EstadoFitosanitario estadofitosanitario);
        Task Disable(EstadoFitosanitario estadofitosanitario);
        Task<EstadoFitosanitario> GetEstadoFitosanitario(int id);
        Task<List<EstadoFitosanitario>> LstEstadoFitosanitario(string search);
    }

    public class EstadoFitosanitarioServices : IEstadoFitosanitario
    {
        public EstadoFitosanitarioServices()
        {

        }
        public async Task<List<EstadoFitosanitario>> LstEstadoFitosanitario(string search)
        {
            try
            {

                using (var sql = Connect.Sql())
                {
                    if (string.IsNullOrWhiteSpace(search))

                        return (await sql.QueryAsync<EstadoFitosanitario>("select* from EstadoFitosanitario where activo = 1 ")).ToList();
                    else
                        return (await sql.QueryAsync<EstadoFitosanitario>(@"select* from Users whereestado active = 1 and like '%' + @search + '%' ", new { search })).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<EstadoFitosanitario> GetEstadoFitosanitario(int Id)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    return await sql.QueryFirstAsync<EstadoFitosanitario>("select* from EstadoFitosanitario where Id = @Id", new { Id });
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al consultar información en DB");
            }
        }
        public async Task<int> Create(EstadoFitosanitario estadofitosanitario)
        {
            int id = 0;
            try
            {
                using (var sql = Connect.Sql())
                {
                    id = await sql.QuerySingleAsync<int>(@"insert into EstadoFitosanitario values (@estado, @activo);select scope_identity();", estadofitosanitario);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return id;
        }
        public async Task Disable(EstadoFitosanitario estadofitosanitario)
        {
            try
            {
                estadofitosanitario.activo = !estadofitosanitario.activo;
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update EstadoFitosanitario set activo = @activo where Id = @Id", estadofitosanitario);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
        }
        public async Task<int> Edit(EstadoFitosanitario estadofitosanitario)
        {
            try
            {
                using (var sql = Connect.Sql())
                {
                    await sql.ExecuteAsync(@"update EstadoFitosanitario set estado = @estado, activo = @activo where Id = @Id", estadofitosanitario);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al guardad información en DB");
            }
            return estadofitosanitario.Id;
        }
    }
}
