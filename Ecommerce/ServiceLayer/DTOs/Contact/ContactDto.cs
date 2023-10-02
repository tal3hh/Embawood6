using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Phone { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
    }
}
