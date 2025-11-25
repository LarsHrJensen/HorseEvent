using Contracts.Integrations;
using EventSchedulingContext.Application.Interfaces;
using EventSchedulingContext.ReadModels;

namespace EventSchedulingContext.Application.Services
{
    public class ClubCreatedEventHandler: IClubCreatedEventHandler
    {
        private readonly IClubReadRepository _repo;

        public ClubCreatedEventHandler(IClubReadRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(ClubCreatedEvent clubCreatedEvent)
        {
            var model = new ClubReadModel
            {
                ClubId = clubCreatedEvent.ClubId,
                Name = clubCreatedEvent.Name,
                DistrictId = clubCreatedEvent.DistrictId
            };

            await _repo.AddAsync(model);
        }
    }
}
