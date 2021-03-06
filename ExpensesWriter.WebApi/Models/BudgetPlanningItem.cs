﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpensesWriter.WebApi.Models
{
    public class BudgetPlanningItem
    {
        public int Id { get; set; }
        public int BudgetItemId { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
        public double Money { get; set; }
        public DateTime PlanningMonth { get; set; }
        public string UserId { get; set;  }
    }
}