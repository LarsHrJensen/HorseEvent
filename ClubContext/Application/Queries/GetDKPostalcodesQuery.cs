using ClubContext.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Application.Queries
{
    public record GetDKPostalCodesQuery() : IRequest<List<PostalCodeDTO>>;
}
