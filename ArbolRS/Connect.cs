using System.Data.SqlClient;

namespace ArbolRS
{
    public static class Connect
    {
        public static string connectionString;
        public static SqlConnection Sql()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            connectionString = builder.GetSection("ConnectionStrings:defaultConnection").Value;

            return new SqlConnection(connectionString);
        }
    }
}
