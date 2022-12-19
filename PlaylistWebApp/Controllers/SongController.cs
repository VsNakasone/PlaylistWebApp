using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaylistWebApp.Repository;

namespace PlaylistWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : Controller
    {
        private readonly SongRepository songRepository;
        public SongController()
        {
            songRepository = new SongRepository();
        }

        [HttpGet]
        [Route("/ListarTodos")]
        public IActionResult ListarTodos()
        {
            try
            {
                var listaSong = songRepository.Listar();
                return Ok(listaSong);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("/Consultar/{id}")]
        public IActionResult Consultar(int idSong)
        {

            try
            {
                var tipoSong = songRepository.Consultar(idSong);
                if (tipoSong.IdSong != 0)
                {
                    return Ok(tipoSong);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("/Cadastrar")]
        public ActionResult Cadastrar(Models.Song song)
        {
            try
            {
                songRepository.Inserir(song);
                return Ok(song);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPatch]
        [Route("/Editar")]
        public ActionResult Editar(Models.Song song)
        {
            try
            {
                songRepository.Alterar(song);
                return Ok(song);
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("/Delete")]
        public ActionResult Delete(int id) 
        {
            try
            {
                songRepository.Excluir(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }
        }
    }
}
