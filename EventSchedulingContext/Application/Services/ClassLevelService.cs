using EventSchedulingContext.Application.DTOs;
using EventSchedulingContext.Application.Interfaces;
using EventSchedulingContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSchedulingContext.Application.Services
{
    public class ClassLevelService : IClassLevelService
    {
        IClassLevelRepository _classLevelRepository;

        public ClassLevelService(IClassLevelRepository classLevelRepository)
        {
            _classLevelRepository = classLevelRepository;
        }

        public async Task<IEnumerable<ClassLevelDTO>> GetAllClassLevelsAsync()
        {
            List<ClassLevel> classLevels = await _classLevelRepository.GetAllAsync();

            return classLevels.Select(cl => new ClassLevelDTO
            {
                Id = cl.Id,
                Name = cl.Name,
                DisciplineId = cl.DisciplineId
            }).ToList();

        }
    }
}
