﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Interfaces.Utils;
using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Interfaces.Services;

namespace Timer_Rubik.WebApp.Controllers.ClientController
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailUtils _emailUtils;
        private readonly IJWTUtils _jWTUtils;
        private readonly IPasswordUtils _passwordUtils;
        private readonly IMapper _mapper;

        public AccountController(IAuthService authService, IAccountRepository accountRepository, IEmailUtils emailUtils, IJWTUtils jWTUtils, IPasswordUtils passwordUtils, IMapper mapper)
        {
            _authService = authService;
            _accountRepository = accountRepository;
            _emailUtils = emailUtils;
            _jWTUtils = jWTUtils;
            _passwordUtils = passwordUtils;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _authService.Login(loginRequest.Email.Trim(), loginRequest.Password.Trim());

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = ex.Message,
                });
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_emailUtils.EmailValid(registerRequest.Email))
                {
                    return BadRequest("Email is invalid");
                }

                if (registerRequest.Password.Length < 6)
                {
                    return BadRequest("Password at least 6 characters");
                }

                if (_accountRepository.GetAccount(registerRequest.Email) != null)
                {
                    return Conflict("Email already exist");
                }

                var accountMap = _mapper.Map<Account>(registerRequest);

                _accountRepository.RegisterAccount(accountMap);

                return Ok("Created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }

        [HttpPatch("forgot")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SendMail([FromBody] SendEmailDTO emailDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_emailUtils.EmailValid(emailDTO.Email))
                {
                    return BadRequest("Email is invalid");
                }

                if (_accountRepository.GetAccount(emailDTO.Email) == null)
                {
                    return NotFound("Not Found Email");
                }

                var account = _accountRepository.GetAccount(emailDTO.Email.Trim());

                string randomPassword = _passwordUtils.GenerateRandomPassword(6);

                _accountRepository.ChangePassword(account.Id, randomPassword);

                _emailUtils.SendEmail(emailDTO.Email, "Reset Password", $"New Password: {randomPassword}");

                return Ok("Email send");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }

        [HttpGet("{accountId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAccount([FromRoute] Guid accountId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var account = _mapper.Map<GetAccountDTO>(_accountRepository.GetAccount(accountId));

                if (account == null)
                {
                    return NotFound("Not Found Account");
                }

                var ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                if (accountId != ownerId)
                {
                    return BadRequest("Id is not match");
                }

                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }

        [HttpPut("{accountId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAccount([FromRoute] Guid accountId, [FromBody] UpdateAccountDTO updateAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (updateAccount.Password.Length < 6)
                {
                    return BadRequest("Password at least 6 characters");
                }

                if (!_accountRepository.AccountExists(accountId))
                {
                    return NotFound("Not Found Account");
                }

                var ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                if (accountId != ownerId)
                {
                    return BadRequest("Id is not match");
                }

                var accountMap = _mapper.Map<Account>(updateAccount);

                _accountRepository.UpdateAccount_User(accountId, accountMap);

                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }
    }
}