using Backend.DTOs.Importer;

namespace Backend.Services.CategoryManagement;

public interface IYearPeriodService
{
    Task<IEnumerable<CategoryDTO>> GetAllService();
}