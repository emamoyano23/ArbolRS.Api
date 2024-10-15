using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolRS.Entity
{
    public class TipoRelevamiento
    {
        public int Id { get; set; }
        public string TipoRelevamientoArbol { get; set; }
        public bool activo { get; set; }
        public string link { get { return $"{access}/{Id}"; } set { } }
        public static string access { get; set; }
        public static void setLink(string value) { access = value; }


    }
}
