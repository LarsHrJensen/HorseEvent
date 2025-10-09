using ClubContext.Application.DTOs;
using MediatR;

namespace ClubContext.Application.Queries
{
    public record GetCountriesQuery() : IRequest<List<CountryDTO>>;
}
