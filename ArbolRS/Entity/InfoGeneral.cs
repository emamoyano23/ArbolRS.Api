using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolRS.Entity
{
    public class InfoGeneral
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int tipoRelevamientoId { get; set; }
        public string nombre { get; set; }
        public int tipoLugarId { get; set; }
        public int DireccionId { get; set; }
        public int AlturaDireccion { get; set; }
        public bool retiroVerde { get; set; }
        public bool activo { get; set; }
        public string link { get { return $"{access}/{Id}"; } set { } }
        public static string access { get; set; }
        public static void setLink(string value) { access = value; }


    }
}
