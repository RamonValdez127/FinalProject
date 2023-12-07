using Microsoft.EntityFrameworkCore;

namespace Nutricion.API.Models
{
    public class Song
    {
        public int id { get; set; }
        public string user { get; set; }
        public string songName { get; set; }
        public string albumCoverLink { get; set; }
        public string artistName { get; set; }
        public string songDuration { get; set; }
        public string displayDuration { get; set; }
        public string displayArtist { get; set; }
    }
}
