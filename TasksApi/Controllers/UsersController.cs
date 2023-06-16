using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksApi.Data;
using TasksApi.Models;

namespace TasksApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UsersAPIDbContext dbContext;

        public UsersController(UsersAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await dbContext.Users.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute]  Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound() ;
            }

            return Ok(user);  
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FullName = addUserRequest.FullName,
                Email = addUserRequest.Email,
                Phone = addUserRequest.Phone,
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UpdateUserRequest updateUserRequest)
        {
            var user = await dbContext.Users.FindAsync (id);

            if(user != null)
            {
                user.FullName = updateUserRequest.FullName;
                user.Email = updateUserRequest.Email;
                user.Phone = updateUserRequest.Phone;

                await dbContext.SaveChangesAsync();

                return Ok(user); 
            }

            return NotFound(); 
        }

        [HttpDelete]
        [Route("{id:guid}")] 
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);

            if(user !=  null)
            {
                dbContext.Remove(user);
                await dbContext.SaveChangesAsync();
                return Ok(user);  
            }

            return NotFound(); 
        }
    }
}

