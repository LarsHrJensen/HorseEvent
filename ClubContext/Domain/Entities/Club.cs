using ClubContext.Domain.ValueObjects;

namespace ClubContext.Domain.Entities
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Adress Adress { get; set; }
    }
}
