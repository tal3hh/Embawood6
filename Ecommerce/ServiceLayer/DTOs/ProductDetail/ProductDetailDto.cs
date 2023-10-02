using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string? Design { get; set; }
        public Colors Color { get; set; }
        public string? Material { get; set; }
        public string? About { get; set; }

        public int ProductId { get; set; }
    }
}
