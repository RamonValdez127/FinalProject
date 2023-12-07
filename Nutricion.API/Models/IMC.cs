using Microsoft.EntityFrameworkCore;

namespace Nutricion.API.Models
{
    public class IMC
    {
        
        public int ID { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }
        public float Resultado { get; set; }
        public DateTime Fecha { get; set; }
        
    }
}
