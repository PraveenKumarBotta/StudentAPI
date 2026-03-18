using Microsoft.AspNetCore.Mvc;
using StudentAPI.Data;
using StudentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPut("{id}")]
       public async Task<IActionResult> Update(int id, Student student)
{
    var existingStudent = await _context.Students.FindAsync(id);

    if (existingStudent == null)
        return NotFound();

    // Update fields manually
    existingStudent.Name = student.Name;
    existingStudent.Course = student.Course;
    existingStudent.Age = student.Age;

    await _context.SaveChangesAsync();

    return Ok(existingStudent);
}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }
    }
}