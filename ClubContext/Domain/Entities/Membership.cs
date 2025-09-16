using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Domain.Entities
{
    internal class Membership
    {
        public int Id { get; set; }
        public Member Member { get; set; }
        public Club Club { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }  
        public string state { get; set; }

    }
}
