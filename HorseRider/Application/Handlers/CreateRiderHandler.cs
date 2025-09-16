using HorseRider.Application.Commands;
using HorseRider.Application.DTO_s;
using HorseRider.Application.Interfaces;
using HorseRider.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                // Opretter og gemmer ny rytter
                var rider = new Rider( command.Name,  command.email, command.BirthYear);
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
