using ExpensesWriter.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ExpensesWriter
{
    public interface IDeviceRegistration
    {
        DeviceRegistration GetDeviceInfo();
    }
}
