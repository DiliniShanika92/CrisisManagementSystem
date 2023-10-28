using AutoMapper;
using CrisisManagementSystem.API.Application.DTOs.Department;
using CrisisManagementSystem.API.Application.DTOs.User;
using CrisisManagementSystem.API.Application.IRepository;
using CrisisManagementSystem.API.Application.Repository;
using CrisisManagementSystem.API.DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrisisManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountController(IAuthManager authManager,ILogger<AccountController> logger, IUserRepository userRepository, IMapper mapper)
        {
            _authManager = authManager;
            this._logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // api/Account/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //swager or  consumer will know there could be 400 error
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        
        public async Task<ActionResult> Register([FromBody] SystemUserDto systemUserDto)
        {
            _logger.LogInformation($"Registration attempt for {systemUserDto.Email}");
            try
            {
                var errors = await _authManager.RegisterUser(systemUserDto);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, $"Something went wrong in the {nameof(Register)}");
                //throw;
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
            
        }

        // api/Account/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //swager or  consumer will know there could be 400 error
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var authResponse = await _authManager.Login(loginDto);

            if (authResponse == null)
            {
               return Unauthorized();
            }
            return Ok(authResponse);
        }


        // api/Account/refreshtoken
        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //swager or  consumer will know there could be 400 error
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
            {
                return Unauthorized();
            }
            return Ok(authResponse);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetUsers()
        {
            if (await _userRepository.GetAllAsync() == null)
            {
                return NotFound();
            }
            var users = await _userRepository.GetAllAsync();

            var getusers = _mapper.Map<List<GetUserDto>>(users);

            return Ok(getusers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SystemUser>> GetUser(int id)
        {
            if (await _userRepository.GetAllAsync() == null)
            {
                return NotFound();
            }
            var user = await _userRepository.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
