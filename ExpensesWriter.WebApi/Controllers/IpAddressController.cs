using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;

namespace ExpensesWriter.WebApi.Controllers
{
    public class IpAddressController : ApiController
    {
        public string GetIpAddress()
        {

            string firstIp =
                $"HttpContext.Current.Request.UserHostAddress: {HttpContext.Current.Request.UserHostAddress}";

            var secondIp = $"GetClientIpAddress(HttpRequestMessage request) {GetClientIpAddress(Request)}";

            var thirdIp = $"HttpContext.Current.Request.Headers[\"X-Forwarded-For\"]: {HttpContext.Current.Request.Headers["X-Forwarded-For"]}";

            return firstIp + Environment.NewLine + secondIp + Environment.NewLine + thirdIp;
        }


        private string GetClientIpAddress(HttpRequestMessage request)
        {
            string HttpContextString = "MS_HttpContext";
            string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

            if (request.Properties.ContainsKey(HttpContextString))
            {
                dynamic ctx = request.Properties[HttpContextString];
                if (ctx != null)
                {
                    return $"ctx.Request.UserHostAddress {ctx.Request.UserHostAddress}";
                }
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return $"remoteEndpoint.Address {remoteEndpoint.Address}";
                }
            }

            if (request.Properties.ContainsKey("MS_OwinContext"))
            {
                string result =  ((OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress;

                return $"OwinContext : {result}";
            }

            return null;
        }
    }
}