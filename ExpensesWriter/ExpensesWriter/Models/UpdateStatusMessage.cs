using ExpensesWriter.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExpensesWriter.Models
{
    public class UpdateStatusMessage
    {
        public UpdateStatus UpdateStatus { get; set; }
        public string Message { get; set; }
        public Color Color { get; set; }


    }
}
