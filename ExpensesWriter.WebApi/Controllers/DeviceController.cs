using ExpensesWriter.WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ExpensesWriter.WebApi.Controllers
{
    [Authorize]
    public class DeviceController : ApiController
    {
        private DevicesContext db = new DevicesContext();

        public async Task<IHttpActionResult> PostDevice(Device device)
        {
            device.UserId = User.Identity.GetUserId();
            var registrationResult = CheckDeviceRegistration(device.DeviceToken);

            try
            {
                await EnsureDeviceRegistration(device, registrationResult);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Saving Device data to database error", ex.Message);
                Trace.WriteLine("Saving Device data to database error", ex.Message);
                throw;
            }

            return Ok();
        }

        [HttpPost, Route("api/device/unregister")]
        public async Task<IHttpActionResult> Delete(Device device)
        {
            Device deviceInDB = db.Devices.Find(device.DeviceToken);

            if (deviceInDB == null)
            {
                return NotFound();
            }

            db.Devices.Remove(deviceInDB);
            await db.SaveChangesAsync();

            return Ok();
        }

        private DeviceRegisteredBefore CheckDeviceRegistration(string deviceToken)
        {
            var device = db.Devices.Find(deviceToken);
            if (device != null)
            {
                if (device.UserId == User.Identity.GetUserId())
                {
                    return DeviceRegisteredBefore.ByThisUser;
                }
                else
                    return DeviceRegisteredBefore.ByOtherUser;
            }

            return DeviceRegisteredBefore.None;
        }

        private async Task EnsureDeviceRegistration(Device device, DeviceRegisteredBefore deviceRegisteredBeforeResult)
        {
            switch (deviceRegisteredBeforeResult)
            {
                case DeviceRegisteredBefore.None:
                    await RegisterDevice(device);
                    break;
                case DeviceRegisteredBefore.ByThisUser:
                    break;
                case DeviceRegisteredBefore.ByOtherUser:
                    await ReregisterDevice(device);
                    break;
            }
        }

        private async Task ReregisterDevice(Device device)
        {
            var updatedDevice = db.Devices.Find(device.DeviceToken);
            updatedDevice.UserId = device.UserId;
            await db.SaveChangesAsync();
        }

        private async Task RegisterDevice(Device device)
        {
            db.Devices.Add(device);
            await db.SaveChangesAsync();
        }

        public enum DeviceRegisteredBefore
        {
            None,
            ByThisUser,
            ByOtherUser
        }
    }
}
