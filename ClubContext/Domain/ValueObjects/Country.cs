using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Domain.ValueObjects
{
    public class Country
    {
        public string Code { get; }  // ISO 3166-1 alpha-2, fx "DK", "US"
        public string Name { get; }  // Full name, fx "Denmark", "United States"
    }
}
