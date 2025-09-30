using HorseEvent.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;


namespace HorseEvent.Data
{
    public class HorseEventDbContext : IdentityDbContext<Users>
    {
        public HorseEventDbContext(DbContextOptions<HorseEventDbContext> options)
        : base(options)
        {
        }
       
    }
}
