using Microsoft.AspNetCore.Mvc;
using ReceitasLiterarias.Models;
using System.Diagnostics;

namespace ReceitasLiterarias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionarComentario([FromBody] Comentario comentario)
        {
            if (string.IsNullOrWhiteSpace(comentario.Texto) || string.IsNullOrWhiteSpace(comentario.NomeReceita))
            {
                return BadRequest("O comentário ou o nome da receita não pode estar vazio.");
            }

            comentario.DataCriacao = DateTime.Now;

            _context.Comentarios.Add(comentario);
            _context.SaveChanges();

            return Ok("Comentário salvo com sucesso.");
        }

        [HttpGet("{nomeReceita}")]
        public IActionResult ListarComentarios(string nomeReceita)
        {
            var comentarios = _context.Comentarios
                .Where(c => c.NomeReceita == nomeReceita)
                .OrderByDescending(c => c.DataCriacao)
                .ToList();

            return Ok(comentarios);
        }
    }

}