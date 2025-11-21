using Contracts.Integrations;

namespace EventSchedulingContext.Application.Interfaces
{
    public interface IClubCreatedEventHandler
    {
        Task Handle(ClubCreatedEvent evt);
    }
}