using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabDiccionarios_1251518.Models
{
    public class MiAlbum {
        public int id { get; set; }
        public string equipo { get; set; }
        public string nombre { get; set; }
        public string disponible { get; set; }
        public int repetidas { get; set; }
    }
}