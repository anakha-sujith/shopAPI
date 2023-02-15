using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Helper;
using ShoppingCartAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ShoppingCartDbContext shoppingCartDbContext;

        public UserController(ShoppingCartDbContext shoppingCartDbContext)
        {
            this.shoppingCartDbContext = shoppingCartDbContext;
        }
        
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(User userobj)
        {
            if (userobj == null)
                return BadRequest();

            var user = await shoppingCartDbContext.UserTable
                .FirstOrDefaultAsync(x => x.UserName == userobj.UserName);

            if (user == null)
                return NotFound(new
                {
                    message = "user not found"
                });
            if (!PasswordHasher.Verifypassword(userobj.Passsword, user.Passsword))
                return BadRequest(new
                {
                    message = "password is incorrect"
                });

            user.Token = CreateJwt(user);
            return Ok(new
            {
                Token = user.Token,
                message = "login success!!"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register( User userObj)
        {
            if (userObj == null)
                return BadRequest();
            if (await CheckusernameExist(userObj.UserName))
                return BadRequest(new
                {
                    message = "username already exists!!!"
                });
            if (await CheckemailExist(userObj.Email))
                return BadRequest(new
                {
                    message = "email already exist!!!"
                });
            var pass = Passwordstrength(userObj.Passsword);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new
                {
                    message = pass.ToString()
                });

            userObj.Passsword = PasswordHasher.HashPassword(userObj.Passsword);
            userObj.Rolee = "user";
            userObj.Token = "";
            await shoppingCartDbContext.UserTable.AddAsync(userObj);
            await shoppingCartDbContext.SaveChangesAsync();
            return Ok(new
            {
                message = "user registered"
            });
        }

        private Task<bool> CheckusernameExist(string username)
            => shoppingCartDbContext.UserTable.AnyAsync(x => x.UserName == username);

        private Task<bool> CheckemailExist(string email)
            => shoppingCartDbContext.UserTable.AnyAsync(x => x.Email == email);

        private string Passwordstrength(string pwd)
        {
            StringBuilder sb = new StringBuilder();
            if (pwd.Length < 8)
                sb.Append("miniminum password should be 8" + Environment.NewLine);
            if (!(Regex.IsMatch(pwd, "[a-z]") && Regex.IsMatch(pwd, "[A-Z]")
                && Regex.IsMatch(pwd, "[0-9]")))
                sb.Append("password should be alphanumeric" + Environment.NewLine);
            if (!Regex.IsMatch(pwd, "[<,>,/,!,@,3,4,%,^,&,*,;,:?,_]"))
                sb.Append("password should contain special characters" + Environment.NewLine);
            return sb.ToString();
        }

        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {

                new Claim(ClaimTypes.Role,user.Rolee),
                new Claim("id",user.UserId.ToString()),
                new Claim(ClaimTypes.Name,$"{user.FirstName}{" "}{user.LastName}")
            }) ;

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<ActionResult<User>> Getallusers()
        {
            return Ok(await shoppingCartDbContext.UserTable.ToListAsync());
        }
       
    }
}
