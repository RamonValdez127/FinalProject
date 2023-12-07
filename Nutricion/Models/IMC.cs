using SQLite;
using System;
using System.Collections.Generic;
//0using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutricion.Models
{
    [Table("imc")]
    public class IMC
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }
        public float Resultado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
