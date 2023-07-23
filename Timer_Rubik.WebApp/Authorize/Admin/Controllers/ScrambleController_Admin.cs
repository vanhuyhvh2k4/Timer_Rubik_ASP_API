﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Attributes;
using Timer_Rubik.WebApp.Authorize.Admin.DTO;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/scramble")]
    public class ScrambleController_Admin : Controller
    {
        private readonly IScrambleService _scrambleService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public ScrambleController_Admin(IScrambleService scrambleService, ICategoryService categoryService, IAccountService accountService, IMapper mapper)
        {
            _scrambleService = scrambleService;
            _categoryService = categoryService;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        [AdminToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetScrambles()
        {
            try
            {
                var scrambles = _scrambleService
                                    .GetScrambles()
                                    .Select(scramble => new
                                    {
                                        Id = scramble.Id,
                                        Category = new
                                        {
                                            Id = scramble.CategoryId,
                                            Name = scramble.Category.Name
                                        },
                                        Account = new
                                        {
                                            Id = scramble.AccountId,
                                            Name = scramble.Account.Name,
                                            Thumbnail = scramble.Account.Thumbnail,
                                            Email = scramble.Account.Email
                                        },
                                        Solve = scramble.Solve?.Answer,
                                        Algorithm = scramble.Algorithm,
                                        Thumbnail = scramble.Thumbnail,
                                        CreatedAt = scramble.CreatedAt,
                                        UpdatedAt = scramble.UpdatedAt,
                                    })
                                    .ToList();

                if (scrambles.Count == 0)
                {
                    return NotFound("Not Found Scramble");
                }

                return Ok(scrambles);
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

        [HttpPut("{scrambleId}")]
        [AdminToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateScramble([FromRoute] Guid scrambleId, [FromBody] UpdateScrambleDTO_Admin updateScramble)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_categoryService.CategoryExists(updateScramble.CategoryId))
                {
                    return NotFound("Not Found Category");
                }

                var categoryMap = _mapper.Map<Scramble>(updateScramble);

                _scrambleService.UpdateScramble(scrambleId, categoryMap);

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

        [HttpDelete("{scrambleId}")]
        [AdminToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteScramble([FromRoute] Guid scrambleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_scrambleService.ScrambleExists(scrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var scrambleEntity = _scrambleService.GetScramble(scrambleId);

                _scrambleService.DeleteScramble(scrambleEntity);

                return Ok("Deleted successfully");
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
