using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class RoleAssingListModel
    {
        public int RoleId { get; set; }
        public string? Name { get; set; }
        public bool Exist { get; set; }
    }
}
