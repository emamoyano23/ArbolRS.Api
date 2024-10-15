using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ArbolRS.Entity
{
    public class Altura
    {
        public int Id { get; set; }
        public string AlturaArbol { get; set; }
        public bool activo { get; set; }
        [JsonIgnore]
        public string link { get { return $"{access}/{Id}"; } set { } }
        public static string access { get; set; }
        public static void setLink(string value) { access = value; }


    }
}
