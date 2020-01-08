using ExpensesWriter.Helpers;
using ExpensesWriter.Models;
using ExpensesWriter.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly LoginService loginService = new LoginService();

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }// "serge2@ukr.net"; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = "Password!1"; }
        }

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        private bool isEnabled = false;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accesstoken = await loginService.LoginAsync(Username, Password);

                    Settings.AccessToken = accesstoken;

                    if (!string.IsNullOrEmpty(accesstoken))
                    {
                        IsEnabled = true;
                        Message = "Success :)";

                        var registerDeviceService = new RegisterDeviceService();
                        bool registerDeviceResult = await registerDeviceService.RegisterDeviceAsync();

                        if (registerDeviceResult)
                            Message = "Device Successfully registered ";
                        else
                            Message = "Device is not registered";

                    }
                    else
                    {
                        Message = "Please try again :(";
                    }




                });
            }
        }

        private void RegisterDevice()
        {
            DeviceRegistration newDevice = new DeviceRegistration();

        }

        public LoginViewModel()
        {
            Username = Settings.Username;
            Password = Settings.Password;
        }
    }
}
