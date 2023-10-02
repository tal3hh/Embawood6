using DomainLayer.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string? Image { get; set; }  
        [NotMapped]
        public IFormFile? Photo { get; set; }



        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<ProductComment>? ProductComments { get; set; }
        public ProductDetail? ProductDetail { get; set; }
    }
}
