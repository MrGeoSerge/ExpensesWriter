using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using ExpensesWriter.Services;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class SSOLoginViewModel : BaseViewModel
    {
        private readonly MicrosoftAuthService _authService;
        private readonly SimpleGraphService _simpleGraphService;

        public bool IsSignedIn { get; set; }
        public bool IsSigningIn { get; set; }
        public string Name { get; set; }

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



        public Command SignInCommand { get; set; }
        public Command SignOutCommand { get; set; }

        public SSOLoginViewModel()
        {
            _authService = new MicrosoftAuthService();
            _simpleGraphService = new SimpleGraphService();

            SignInCommand = new Command(() => SignInAsync());
            SignOutCommand = new Command(() => SignOutAsync());
        }



        async Task SignInAsync()
        {
            IsSigningIn = true;

            if (await _authService.SignInAsync())
            {
                Name = await _simpleGraphService.GetNameAsync();
                Debug.WriteLine($"SSOLoginViewModel SignInAsync() IsSignedIn = true");
                Debug.WriteLine($"SSOLoginViewModel SignInAsync() {Name}");

                if(Name == "velychko.serge@gmail.com")
                {
                    await new LoadDataService().LoadData();

                    var registerDeviceService = new RegisterDeviceService();
                    bool registerDeviceResult = await registerDeviceService.RegisterDeviceAsync();

                    if (registerDeviceResult)
                        Message = "Device Successfully registered ";
                    else
                        Message = "Device is not registered";

                }

                IsSignedIn = true;
            }

            IsSigningIn = false;
        }

        async Task SignOutAsync()
        {
            if (await _authService.SignOutAsync())
            {
                IsSignedIn = false;
            }
        }


    }
}
