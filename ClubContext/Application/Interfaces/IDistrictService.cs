using ClubContext.Application.DTOs;

namespace BackendAPI.Controllers.ClubControllers
{
    public interface IDistrictService
    {
        Task<IEnumerable<DistrictDTO>> GetAllDistrictsAsync();
    }
}