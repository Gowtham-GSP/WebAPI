using Employees.Data;
using Employees.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        public readonly EmployeeDbContext dbContext;

        public EmployeeController(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //---------------------------Get All -------------------------------
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await dbContext.Employees.ToListAsync());
        }

        //---------------------------Post or create-----------------------------
        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeRequest request)
        {
            var Employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                state = request.state,
                Country = request.Country,
            };

            await dbContext.Employees.AddAsync(Employee);
            await dbContext.SaveChangesAsync();
            return Ok(Employee);
        }
        //------------------------------Get One-----------------------------------
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return BadRequest();
            }
             return Ok(employee);
        }

        // ---------------------------------Update-------------------------------
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id,UpdateEmployeeRequest updateEmployee)
        {

            var Employee = await dbContext.Employees.FindAsync(id);
            if(Employee != null)
            {
                Employee.Name = updateEmployee.Name;
                Employee.Email = updateEmployee.Email;
                Employee.Phone = updateEmployee.Phone;
                Employee.Address = updateEmployee.Address;
                Employee.state = updateEmployee.state;
                Employee.Country = updateEmployee.Country;

                await dbContext.SaveChangesAsync();
                return Ok(Employee);
            }
            return NotFound();
        }
        //-------------------------------Remove------------------------------
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
          var Employee = await dbContext.Employees.FindAsync(id);

          if(Employee != null)
              {
                dbContext.Remove(Employee);
                await dbContext.SaveChangesAsync();
                return Ok(Employee);
              }
          return BadRequest();
        }
    }
}
