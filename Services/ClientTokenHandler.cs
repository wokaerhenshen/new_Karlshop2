using Braintree;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Services
{
    public class ClientTokenHandler 
    {
        public static string ProcessRequest(HttpContext context)
        {
            BraintreeGateway gateway = new BraintreeGateway("access_token$sandbox$ym2fhwysk723gtg4$2e18ce46a546af041a2f90f2d46eb441");
            var clientToken = gateway.ClientToken.Generate();
            context.Response.WriteAsync(clientToken);
            return clientToken;
        }
    }
}
