using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace ExpensesWriter.Models
{
    [Table("FamilyMembers")]
    public class FamilyMember
    {
        [PrimaryKey]
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
