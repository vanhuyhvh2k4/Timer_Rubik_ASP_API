﻿using Timer_Rubik.WebApp.DTO.Client;

namespace Timer_Rubik.WebApp.Interfaces.Services
{
    public interface IcategoryService
    {
        APIResponseDTO<ICollection<GetCategoryDTO>> GetCategories();

        APIResponseDTO<GetCategoryDTO> GetCategory(Guid categoryId);
    }
}
