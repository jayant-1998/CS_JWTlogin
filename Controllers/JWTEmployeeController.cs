using JWTLogin.Models.RequestViewModels;
using JWTLogin.Models.ResponseViewModels;
using JWTLogin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JWTLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JWTEmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        public JWTEmployeeController(IServiceProvider serviceProvider)
        {
            _service = serviceProvider.GetRequiredService<IEmployeeService>();
        }
        // POST: insert data
        [HttpPost("registration")]
        public async Task<IActionResult> Registration(RegistrationRequestViewModel reg)
        {
            try
            {
                RegistrationResponseViewModel result = await _service.RegistrationAsync(reg);
                var response = new ApiResponseViewModel
                {
                    Code = 200,
                    Message = "Success",
                    Body = result
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };
                return Ok(response);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestViewModel login)
        {
            try
            {
                var result = await _service.LoginAsync(login);

                var response = new ApiResponseViewModel
                {
                    Code = 200,
                    Message = "Success",
                    Body = result
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };
                return Ok(response);
            }
        }


    }
}