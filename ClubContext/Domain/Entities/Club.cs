using ClubContext.Domain.ValueObjects;

namespace ClubContext.Domain.Entities
{
   
    public class Club
    {
        public int ClubId { get; set; }
        public string Name { get; set; }
        public Adress Adress { get; set; }
        public int? DistrictId { get; set; }

        public void SetAdress(Adress adress) => Adress = adress;
    }
    
}
