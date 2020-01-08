using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpensesWriter.WebApi.Models
{
    public class Device
    {
        [Key]
        [Required]
        [StringLength(160)]
        public string DeviceToken { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Platform { get; set; }

    }
}