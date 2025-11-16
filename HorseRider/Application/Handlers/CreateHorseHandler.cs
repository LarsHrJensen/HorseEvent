using HorseRider.Application.Commands;
using HorseRider.Application.DTO_s;
using HorseRider.Application.Interfaces;
using HorseRider.Domain.Entities;
using MediatR;

namespace HorseRider.Application.Handlers
{
    public class CreateHorseHandler : IRequestHandler<CreateHorseCommand, HorseDTO>
    {
        private readonly IHorseRepository _horseRepository;

        public CreateHorseHandler(IHorseRepository horseRepository)
            => _horseRepository = horseRepository;

        public async Task<HorseDTO> Handle(CreateHorseCommand command, CancellationToken cancellationToken)
        {
            // Opretter og gemmer ny hest
            var horse = new Horse(command.Name, command.UELN, command.Height, command.BirthYear);
            if (command.Gender != null) { horse.Gender = command.Gender; }
            if (command.Color != null) { horse.Color = command.Color; }
            if (command.Breed != null) { horse.Breed = command.Breed; }
            if (command.Breeder != null) { horse.Breeder = command.Breeder; }
            if (command.SireId != null) { horse.SireId = command.SireId; }
            if (command.DamId != null) { horse.DamId = command.DamId; }

            

            horse.HorseId =await _horseRepository.AddAsync(horse);

            // Mapper til DTO direkte
            return new HorseDTO
            {
                Id = horse.HorseId,
                HorseName = horse.Name,
                UELN = horse.UELN,
                HorseHeight = horse.Height,
                BirthYear = horse.BirthYear
            };
        }
    }
}
