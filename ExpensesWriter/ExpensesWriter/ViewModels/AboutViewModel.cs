using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Input;
using ExpensesWriter.Services;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));

            _ = GetIpAddress();
        }

        private async Task GetIpAddress()
        {
            IpAddress = await new IpAddressService().GetIpAddress();
        }


        private string ipAddress;

        public String IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenWebCommand { get; }
    }
}