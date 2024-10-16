using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolRS.Entity
{
    public class Arbol
    {
        public int Id { get; set; }
        public int AlturaId { get; set; }
        public int AnchoId { get; set; }
        public int EdadId { get; set; }
        public string Foto { get; set; }
        public bool ArbolHistorico { get; set; }
        public int InfoGeneralId { get; set; }
        public bool activo { get; set; }
        public string link { get { return $"{access}/{Id}"; } set { } }
        public static string access { get; set; }
        public static void setLink(string value) { access = value; }


    }
}
