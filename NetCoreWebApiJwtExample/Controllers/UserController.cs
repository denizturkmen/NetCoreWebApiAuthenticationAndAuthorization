using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiJwtExample.Business.Abstract;
using NetCoreWebApiJwtExample.JwtIdentity;
using NetCoreWebApiJwtExample.Models;

namespace NetCoreWebApiJwtExample.Controllers
{
    //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    [Authorize]
    [ApiController]
    //Routing için mesela /Sample/GetSummaries olarak ayarladım.
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private static readonly string[] Summaries =
            {"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};

        private IUserService _userService;
        private ApplicationDbContext _context;

        public UserController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }


        //Burada da AllowAnonymous attribute nü kullanarak bu seferde bu metoda herkesin erişebileceğini söylüyorum.
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody]AuthenticateModel authenticateModel)
        {
            var user = _userService.Authenticate(authenticateModel.Username, authenticateModel.Password);

            if (user == null)
                return BadRequest("Username or password incorrect!");

            //return Ok(new { Username = user.Value.username, Token = user.Value.token });
            return Ok(new
            {
                UserName = user.Value.username,
                Token = user.Value.token,

            });
        }

        [HttpGet]
        public IActionResult GetSummaries() => Ok(Summaries);

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody] AuthenticateModel model)
        {
            if (model == null)
            {
                return NotFound("Yokk");
            }
            var user = new ApplicationUser()
            {
                Password = model.Password,
                Username = model.Username
            };

            _context.ApplicationUsers.Add(user);
            _context.SaveChanges();

            return Ok();
        }


    }
}