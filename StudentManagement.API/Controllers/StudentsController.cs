using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.API.Data;
using StudentManagement.API.Models;

namespace StudentManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todos os estudantes
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        var students = await _context.Students.ToListAsync();
        return Ok(students);
    }

    /// <summary>
    /// Retorna um estudante específico por ID
    /// </summary>
    /// <param name="id">ID do estudante</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound(new { message = $"Estudante com ID {id} não encontrado" });
        }

        return Ok(student);
    }

    /// <summary>
    /// Cria um novo estudante
    /// </summary>
    /// <param name="student">Dados do estudante</param>
    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    /// <summary>
    /// Atualiza um estudante existente
    /// </summary>
    /// <param name="id">ID do estudante</param>
    /// <param name="student">Dados atualizados do estudante</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student student)
    {
        if (id != student.Id)
        {
            return BadRequest(new { message = "ID do estudante não corresponde" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingStudent = await _context.Students.FindAsync(id);
        if (existingStudent == null)
        {
            return NotFound(new { message = $"Estudante com ID {id} não encontrado" });
        }

        existingStudent.Nome = student.Nome;
        existingStudent.Idade = student.Idade;
        existingStudent.Serie = student.Serie;
        existingStudent.NotaMedia = student.NotaMedia;
        existingStudent.Endereco = student.Endereco;
        existingStudent.NomePai = student.NomePai;
        existingStudent.NomeMae = student.NomeMae;
        existingStudent.DataNascimento = student.DataNascimento;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(id))
            {
                return NotFound(new { message = $"Estudante com ID {id} não encontrado" });
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Deleta um estudante
    /// </summary>
    /// <param name="id">ID do estudante</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound(new { message = $"Estudante com ID {id} não encontrado" });
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.Id == id);
    }
}
