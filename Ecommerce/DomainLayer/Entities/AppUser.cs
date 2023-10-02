using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? Fullname { get; set; }
        public DateTime CreateDate { get; set; }

        public List<Favorite>? Favorites { get; set; }
        public List<Basket>? Baskets { get; set; }
    }
}
