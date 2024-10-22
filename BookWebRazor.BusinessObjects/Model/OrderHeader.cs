using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.BusinessObjects.Model
{
    public class OrderHeader : BaseEntity
    {        
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        [ValidateNever]
        public Account Account { get; set; } = null!;

        public DateTime OrderDate { get; set; }
        public double OrderTotal { get; set; }
        public string? OrderStatus { get; set; }
    }
}
