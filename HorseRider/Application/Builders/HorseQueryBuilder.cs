using HorseRider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRiderContext.Application.Builders
{
    public class HorseQueryBuilder
    {
        private IQueryable<Horse> _query;

        public HorseQueryBuilder(IQueryable<Horse> initialQuery)
        {
            _query = initialQuery;
        }

        public HorseQueryBuilder FilterByName(string? name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                _query = _query.Where(h => h.Name.Contains(name));
            return this;
        }

        public HorseQueryBuilder FilterByUELN(string? ueln)
        {
            if (!string.IsNullOrWhiteSpace(ueln))
                _query = _query.Where(h => h.UELN.Contains(ueln));
            return this;
        }

        public HorseQueryBuilder FilterByBirthYear(int? birthYear)
        {
            if (birthYear.HasValue)
                _query = _query.Where(h => h.BirthYear == birthYear.Value);
            return this;
        }

        public HorseQueryBuilder FilterByRaceId(int? raceId)
        {
            if (raceId.HasValue)
                _query = _query.Where(h => h.BreedId == raceId.Value);
            return this;
        }

        public IQueryable<Horse> Build() => _query;
    }

}
