using BackendAPI.Controllers.ClubControllers;
using ClubContext.Application.DTOs;
using ClubContext.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Application.Services
{
    public class DistrictService : IDistrictService
    {
        private IDistrictRepository _districtRepository;

        public DistrictService(IDistrictRepository districtRepository) {
            _districtRepository = districtRepository;
        }
        public async Task<IEnumerable<DistrictDTO>> GetAllDistrictsAsync()
        {
           var districts = await _districtRepository.GetAllAsync();

            //map entitiy to dto
            return districts.Select(d => new DistrictDTO
            {
                DistrictId = d.DistrictId,
                Name = d.Name,
                CountryCode = d.CountryCode
            }).ToList();

        }
    }
}
