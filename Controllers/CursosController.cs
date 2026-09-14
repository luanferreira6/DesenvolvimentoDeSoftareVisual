using Microsoft.AspNetCore.Mvc;
using CursosAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace CursosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {
        // Lista estática em memória para armazenar os dados (Etapa 3)
        private static List<Curso> cursos = new List<Curso>();

        // ETAPA 1 - GET: Consultar todos os cursos
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(cursos);
        }

        // ETAPA 1 - GET: Consultar curso específico pelo Id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var curso = cursos.FirstOrDefault(c => c.Id == id);
            if (curso == null)
            {
                return NotFound(new { mensagem = "Curso não encontrado." });
            }
            return Ok(curso);
        }

        // ETAPA 2 - POST: Cadastrar novo curso
        [HttpPost]
        public IActionResult Post([FromBody] Curso novoCurso)
        {
            cursos.Add(novoCurso);
            // Retorna o status 201 Created e a rota para acessar o item recém-criado
            return CreatedAtAction(nameof(GetById), new { id = novoCurso.Id }, novoCurso);
        }

        // ETAPA 3 - PUT: Atualizar curso existente
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Curso cursoAtualizado)
        {
            var curso = cursos.FirstOrDefault(c => c.Id == id);
            if (curso == null)
            {
                return NotFound(new { mensagem = "Curso não encontrado para atualização." });
            }

            curso.Nome = cursoAtualizado.Nome;
            curso.CargaHoraria = cursoAtualizado.CargaHoraria;

            return NoContent(); // Status 204: Sucesso, mas sem conteúdo de retorno
        }

        // ETAPA 4 - DELETE: Excluir um curso
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var curso = cursos.FirstOrDefault(c => c.Id == id);
            if (curso == null)
            {
                return NotFound(new { mensagem = "Curso não encontrado para exclusão." });
            }

            cursos.Remove(curso);
            return NoContent();
        }
    }
}