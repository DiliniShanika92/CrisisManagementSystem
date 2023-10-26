
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrisisManagementSystem.API.DataLayer;
using CrisisManagementSystem.API.Application.DTOs.Department;
using AutoMapper;
using CrisisManagementSystem.API.Application.IRepository;

namespace CrisisManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;

        /// <summary>
        /// Initializes a new instance of the DepartmentsController class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="departmentRepository">The repository for managing departments.</param>
        public DepartmentsController(IMapper mapper,IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Get a list of all departments.
        /// </summary>
        /// <returns>An ActionResult containing a list of department DTOs.</returns>
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

        /// <summary>
        /// Get a specific department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to retrieve.</param>
        /// <returns>An ActionResult containing the department entity if found; otherwise, NotFound.</returns>
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

        /// <summary>
        /// Update a specific department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to update.</param>
        /// <param name="updateDepartment">The data for updating the department.</param>
        /// <returns>An IActionResult representing the outcome of the operation (NoContent, BadRequest, NotFound, etc.).</returns>
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

        /// <summary>
        /// Create a new department.
        /// </summary>
        /// <param name="createDepartment">The data for creating a new department.</param>
        /// <returns>An ActionResult containing the newly created department entity or a URL to access it.</returns>
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(CreateDepartmentDto createDepartment)
        {
          var department = _mapper.Map<Department>(createDepartment);

            await _departmentRepository.AddAsync(department);

            return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
        }

        /// <summary>
        /// Delete a specific department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        /// <returns>An IActionResult representing the outcome of the operation (NoContent, NotFound, etc.).</returns>
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
        /// <summary>
        /// Check if a department with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the department to check for existence.</param>
        /// <returns>A boolean indicating whether the department exists (true) or not (false).</returns>
        private async Task<bool> DepartmentExists(int id)
        {
            return await _departmentRepository.Exists(id);
        }
    }
}
