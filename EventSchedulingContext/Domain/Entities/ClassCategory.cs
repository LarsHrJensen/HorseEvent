using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSchedulingContext.Domain.Entities
{
    public class ClassCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisciplineId { get; set; }
    }
}
