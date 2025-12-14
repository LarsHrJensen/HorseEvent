using HorseRider.Application.Commands;
using HorseRider.Application.DTO_s;
using HorseRider.Application.Interfaces;
using HorseRider.Domain.Entities;
using Ganss.Xss;

using MediatR;

namespace HorseRider.Application.Handlers
{
    namespace HorseRider.Application.Handlers
    {
        public class CreateRiderHandler : IRequestHandler<CreateRiderCommand, RiderDTO>
        {
            private readonly IRiderRepository _riderRepository;

            public CreateRiderHandler(IRiderRepository riderRepository)
                => _riderRepository = riderRepository;

            public async Task<RiderDTO> Handle(CreateRiderCommand command, CancellationToken cancellationToken)
            {
                // SANITIZATION AF BRUGER INPUT FRA FRONTEND
                var sanitizer = new HtmlSanitizer();

                var cleanName = sanitizer.Sanitize(command.Name);
                var cleanEmail = sanitizer.Sanitize(command.email);
                var cleanBirthYear = command.BirthYear;

                // Opretter og gemmer ny rytter
                var rider = new Rider(cleanName, cleanEmail, cleanBirthYear);
                await _riderRepository.AddAsync(rider);

                // Mapper til DTO direkte
                return new RiderDTO
                {
                    RiderName = rider.RiderName,
                    Email = rider.Email,
                    BirthYear = rider.BirthYear
                };
            }
        }
    }
}
