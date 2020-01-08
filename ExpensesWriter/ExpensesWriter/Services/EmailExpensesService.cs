using ExpensesWriter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExpensesWriter.Services
{
    public class EmailExpensesService
    {
        public async Task SendExpenses(IEnumerable<Expense> expenses)
        {
            string messageBody = ComposeMessageBody(expenses);

            try
            {
                var message = new EmailMessage
                {
                    Subject = "Expenses",
                    Body = messageBody,
                    To = new List<string> { "velychko@ukr.net" }
                };

                await Email.ComposeAsync(message);
            }
            catch(FeatureNotSupportedException fnsEx)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("FeatureNotSupported", fnsEx.Message, "Ok");
                });

            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("Error Sending Email", ex.Message, "Ok");
                });

            }
        }

        private string ComposeMessageBody(IEnumerable<Expense> expenses)
        {
            StringBuilder messageBody = new StringBuilder();
            foreach (var expense in expenses)
            {
                EmailExpense emailExpense = new EmailExpense(expense);
                messageBody.AppendLine(emailExpense.ToString()); //LineBreak is not working on Android, it removes all special characters
            }
            return messageBody.ToString();
        }
    }
}
