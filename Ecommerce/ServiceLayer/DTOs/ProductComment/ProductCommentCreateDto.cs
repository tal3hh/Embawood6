using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class ProductCommentCreateDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }

        public int ProductId { get; set; }
    }
}
