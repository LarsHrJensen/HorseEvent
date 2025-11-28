using HorseRider.Application.DTO_s;
using MediatR;

namespace HorseRider.Application.Commands
{
    public record CreateHorseCommand(
        string Name,
        string UELN,
        int Height,
        int BirthYear,
        string? Gender,            // Hoppe / Vallak / Hingst
        string? Color,         // Frivillig
        int? Breed,         // Frivillig (race / avlsforbund)
        string? Breeder,       // Frivillig (avler)
        int? SireId,        // Frivillig
        int? DamId         // Frivillig
    ) : IRequest<HorseDTO>;
}
