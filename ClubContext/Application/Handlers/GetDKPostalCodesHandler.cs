using ClubContext.Application.DTOs;
using ClubContext.Application.Interfaces;
using ClubContext.Application.Queries;
using MediatR;

namespace ClubContext.Application.Handlers
{
    internal class GetDKPostalCodesHandler : IRequestHandler<GetDKPostalCodesQuery, List<PostalCodeDTO>>
    {
        private readonly IPostalCodeRepository _postalcodeRepository;

        public GetDKPostalCodesHandler(IPostalCodeRepository _postalCodeRepository)
        {
           this._postalcodeRepository = _postalCodeRepository;
        }

        public async Task<List<PostalCodeDTO>> Handle(GetDKPostalCodesQuery request, CancellationToken cancellationToken)
        {
            var postalCodes = await _postalcodeRepository.GetAllAsync();

            return postalCodes.Select(pc => new PostalCodeDTO
            {
                PostalCode = pc.PostalCode,
                City = pc.City

            }).ToList();
        }
    }
}

