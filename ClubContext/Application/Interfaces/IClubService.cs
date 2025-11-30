using ClubContext.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Application.Interfaces
{
    public interface IClubService
    {
        Task<ClubDto> CreateClubAsync(string name, AddressDto address, int? districtId);
        Task<ClubDto?> GetClubByIdAsync(int id);
        Task<IEnumerable<ClubDto>> GetAllClubsAsync();
       
    }
}
