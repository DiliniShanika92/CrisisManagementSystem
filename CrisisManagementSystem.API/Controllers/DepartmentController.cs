
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrisisManagementSystem.API.DataLayer;
using CrisisManagementSystem.API.Application.DTOs.Department;
using AutoMapper;
using CrisisManagementSystem.API.Application.IRepository;
using CrisisManagementSystem.API.Application.DTOs.User;

namespace CrisisManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;

        //since we register our dbcontext with builder.services in program.cs
        //it geives the ability to inject almost anyfile wewant
        public DepartmentsController(IMapper mapper,IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetDepartments()
        {
          if (await _departmentRepository.GetAllAsync() == null)
          {
              return NotFound();
          }
            var departments = await _departmentRepository.GetAllAsync();

            var getdepartments = _mapper.Map<List<GetDepartmentDto>>(departments);

            return Ok(getdepartments); 
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
          if (await _departmentRepository.GetAllAsync() == null)
          {
              return NotFound();
          }
            var department = await _departmentRepository.GetAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, UpdateDepartmentDto updateDepartment)
        {
            if (id != updateDepartment.Id)
            {
                return BadRequest();
            }

            // _context.Entry(updateDepartment).State = EntityState.Modified;

            var department = await _departmentRepository.GetAsync(id);//from this line entity get tracked.
            if (department == null)
            {
                return NotFound();
            }

            _mapper.Map(updateDepartment,department); //from this line it get modified

            try
            {
                await _departmentRepository.UpdateAsync(department);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(CreateDepartmentDto createDepartment)
        {
          var department = _mapper.Map<Department>(createDepartment);

            await _departmentRepository.AddAsync(department);

            return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (await _departmentRepository.GetAllAsync() == null)
            {
                return NotFound();
            }
            var department = await _departmentRepository.GetAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            await _departmentRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> DepartmentExists(int id)
        {
            return await _departmentRepository.Exists(id);
        }
    }
}
