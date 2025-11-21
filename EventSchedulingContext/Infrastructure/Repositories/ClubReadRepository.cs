using EventSchedulingContext.Application.Interfaces;
using EventSchedulingContext.Infrastructure;
using EventSchedulingContext.ReadModels;
using Microsoft.EntityFrameworkCore;

public class ClubReadRepository : IClubReadRepository
{
    private readonly EventSchedulingDbContext _db;

    public ClubReadRepository(EventSchedulingDbContext db)
    {
        _db = db;
    }

    public async Task<int> AddAsync(ClubReadModel entity)
    {
        _db.Clubs.Add(entity);
        await _db.SaveChangesAsync();
        return entity.ClubId;
    }

    public async Task UpdateAsync(ClubReadModel entity)
    {
        _db.Clubs.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(ClubReadModel entity)
    {
        _db.Clubs.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<List<ClubReadModel>> GetAllAsync()
    {
        return await _db.Clubs.ToListAsync();
    }

    public async Task<ClubReadModel?> GetByIdAsync(int id)
    {
        return await _db.Clubs.FirstOrDefaultAsync(x => x.ClubId == id);
    }
}
