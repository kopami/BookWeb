using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.BusinessObjects.Model
{
    public class Category : BaseEntity
    {
        [Key]
        public override int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Display Order")]
        [Range(0, 100, ErrorMessage = "The field Display Order must be between 0 - 100")]
        public int DisplayOrder { get; set; }
    }
}
