using ArbolRS.Controllers;
using ArbolRS.Data;
using ArbolRSData;

namespace ArbolRS
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IAltura, AlturaServices>();
            services.AddScoped<IAncho, AnchoServices>();
            services.AddScoped<INombreArbol, NombreArbolServices>();
            services.AddScoped<ICazuela, CazuelaServices>();
            services.AddScoped<ITipoLugar, TipoLugarServices>();
            services.AddScoped<ITipoRelevamiento, TipoRelevamientoServices>();
            services.AddScoped<IEstadoFitosanitario,EstadoFitosanitarioServices>();
            services.AddScoped<IEdad, EdadServices>();
            services.AddScoped<IFaseVital, FaseVitalServices>();
            services.AddScoped<IInclinacion, InclinacionServices>();
            services.AddScoped<IArbol, ArbolServices>();
            services.AddScoped<IInfoGeneral, InfoGeneralServices>();
            services.AddScoped<IInfoAvanzada, InfoAvanzadaServices>();





        }
    }
}
