using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutricion.API.Models;
using Nutricion.API.Data;

namespace Nutricion.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public SongController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        [HttpPost("registrar")]

        public async Task<ActionResult<int>> Guardar(Song song)
        {
            var newSong = song;
            context.Add(newSong);

            await context.SaveChangesAsync();

            if (newSong.id >= 0)
            {
                return newSong.id;
            }
            else
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("lista")]

        public async Task<ActionResult<List<Song>>> Registros()
        {
            var lista = new List<Song>();
            lista = await context.registroSongs.ToListAsync();
            if (lista.Count > 0)
            {
                return lista;
            }
            else
            {
                return NoContent();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteSong(int id)
        {
            var delSong = await context.registroSongs.FindAsync(id);
            if (delSong == null)
                return NotFound();
            context.registroSongs.Remove(delSong);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
