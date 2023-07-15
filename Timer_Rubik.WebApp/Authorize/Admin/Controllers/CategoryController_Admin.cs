﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.Admin.DTO;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/category")]
    public class CategoryController_Admin : Controller
    {
        private readonly ICategoryService_Admin _categoryService_Admin;
        private readonly IMapper _mapper;

        public CategoryController_Admin(ICategoryService_Admin categoryRepository_Admin, IMapper mapper)
        {
            _categoryService_Admin = categoryRepository_Admin;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Index()
        {
            try
            {
                var categories = _mapper.Map<List<GetCategoryDTO_Admin>>(_categoryService_Admin.GetCategories());

                if (categories.Count == 0)
                {
                    return NotFound("Not Found Category");
                }

                return Ok(categories);
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

        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCategory([FromRoute] Guid categoryId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var category = _mapper.Map<GetCategoryDTO_Admin>(_categoryService_Admin.GetCategory(categoryId));

                if (category == null)
                {
                    return NotFound("Not Found Category");
                }

                return Ok(category);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CreateCategoryDTO_Admin createCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_categoryService_Admin.GetCategory(createCategory.Name) != null)
                {
                    return Conflict("Name Already Exists");
                }

                var categoryMap = _mapper.Map<Category>(createCategory);

                _categoryService_Admin.CreateCategory(categoryMap);

                return Ok("Created successfully");
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }

        [HttpPatch("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory([FromRoute] Guid categoryId, [FromBody] UpdateCategoryDTO_Admin updateCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (categoryId != updateCategory.Id)
                {
                    return BadRequest("Id is not match");
                }

                if (!_categoryService_Admin.CategoryExists(categoryId))
                {
                    return NotFound("Not Found Category");
                }

                if (_categoryService_Admin.GetCategory(updateCategory.Name) != null)
                {
                    return Conflict("Name already exists");
                }

                var categoryMap = _mapper.Map<Category>(updateCategory);

                _categoryService_Admin.UpdateCategory(categoryMap);

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

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory([FromRoute] Guid categoryId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_categoryService_Admin.CategoryExists(categoryId))
                {
                    return NotFound("Not Found Category");
                }

                var categoryEntity = _categoryService_Admin.GetCategory(categoryId);

                _categoryService_Admin.DeleteCategory(categoryEntity);

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
