using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolRS.Entity
{
    public class InfoAvanzada
    {
        public int Id { get; set; }
        public int ArbolId { get; set; }
        public int FaseVitalId { get; set; }
        public int EstadoFitosanitarioId { get; set; }
        public int InclinacionId { get; set; }
        public int CazuelaId { get; set; }
        public bool activo { get; set; }
        public string link { get { return $"{access}/{Id}"; } set { } }
        public static string access { get; set; }
        public static void setLink(string value) { access = value; }


    }
}
