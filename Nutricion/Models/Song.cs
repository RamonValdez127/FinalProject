using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutricion.Models;

[Table("song")]
public class Song
{
    [PrimaryKey, AutoIncrement, Unique]
    public int id { get; set; }
    public string user { get; set; }
    public string songName { get; set; }
    public string albumCoverLink { get; set; }
    public string artistName { get; set; }
    public string songDuration { get; set; }
    public string displayDuration { get; set; }
    public string displayArtist { get; set; }
}
