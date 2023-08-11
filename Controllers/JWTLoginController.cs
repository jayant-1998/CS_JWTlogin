using JWTLogin.Models.RequestViewModels;
using JWTLogin.Models.ResponseViewModels;
using JWTLogin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JWTLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JWTLoginController : ControllerBase
    {
        private readonly IJWTLoginService _service;
        public JWTLoginController(IServiceProvider serviceProvider)
        {
            _service = serviceProvider.GetRequiredService<IJWTLoginService>();
        }
        // POST: insert data
        [HttpPost("registration")]
        public async Task<IActionResult> InsertData(RegistrationRequestViewModel reg)
        {
            try
            {
                var result = await _service.InsertDataAsync(reg);
                

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
        [HttpGet("login")]
        public async Task<IActionResult> Login(LoginRequestViewModel login)
        {
            try
            {
                var result = _service.LoginAsync(login);

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