using ExpensesWriter.Helpers;
using ExpensesWriter.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly RegisterService registerService = new RegisterService();

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        private string message;
        public string Message {
            get {
                return message;
            }
            set { message = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isRegistered = await registerService.RegisterUserAsync
                        (Username, Password, ConfirmPassword);

                    Settings.Username = Username;
                    Settings.Password = Password;

                    if (isRegistered)
                    {
                        Message = "Success :)";
                    }
                    else
                    {
                        Message = "Please try again :(";
                    }
                });
            }
        }


    }
}
