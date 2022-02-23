using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2.model
{

    class UbicacionModel
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        
        [MaxLength(600)]
        public string nombre { get; set; }
        [MaxLength(70)]
        public string descripcion { get; set; }
        [MaxLength(100)]
        public byte[] fotografia { get; set; }
    }
}
