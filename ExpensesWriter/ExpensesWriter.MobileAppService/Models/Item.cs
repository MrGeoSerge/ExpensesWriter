using System;
using System.ComponentModel.DataAnnotations;

namespace ExpensesWriter.Models
{
    public class Item
    {
        public string Id { get; set; }

        [Required]
        public double Money { get; set; }

        [Required]
        public string Name { get; set; }

        public string Category { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ModificationDateTime { get; set; }

        public string UserId { get; set; }

    }
}
