
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Paypal
{
    public class PaypalAPI
    {
        public IConfiguration Configuration { get; }
        public PaypalAPI(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<string> getRedirectUrlToPaypal(double total, string currency)
        {
            try
            {
                return  Task.Run(async () =>
                 {
                     HttpClient http = GetPaypalHttpClient();

                    // Step 1: Get an access token
                    PayPalAccessToken accessToken = await GetPayPalAccessTokenAsync(http);
                    //Log.Information("Access Token \n{@accessToken}", accessToken);

                    // Step 2: Create the payment
                    PayPalPaymentCreatedResponse createdPayment = await CreatePaypalPaymentAsync(http, accessToken,total,currency);
                    //Log.Information("Created payment \n{@createdPayment}", createdPayment);

                    // Step 3: Get the approval_url and paste it into a browser
                    // It should look something like this: https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=EC-97965369EL8295114
                    //var approval_url = createdPayment.links.First(x => x.rel == "approval_url").href;
                    //Log.Information("approval_url\n{approval_url}", approval_url);

                    ////
                    //// IMPORTANT: Stop the program here, and re-run only the section below (comment out Step 2 and Step 3) and paste in the correct paymentId and payerId
                    ////

                    //// Step 4: When paypal redirects to the return_url, we need to grab the PayerID and the paymentId and execute the payment
                    //var paymentId = "PAY-9LN814307S704373KK6UFTHI";
                    //var payerId = "LMWV7AASBDUQQ";

                    //PayPalPaymentExecutedResponse executedPayment = await ExecutePaypalPaymentAsync(http, accessToken, paymentId, payerId);
                    //Log.Information("Executed payment \n{@executedPayment}", executedPayment);
                    return createdPayment.links.First(x => x.rel == "approval_url").href;
                 }).Result;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to login to PalPal");
                return null;
            }


        }

        private  HttpClient GetPaypalHttpClient()
        {
             string sandbox = Configuration["Paypal:urlAPI"];

            var http = new HttpClient
            {
                BaseAddress = new Uri(sandbox),
                Timeout = TimeSpan.FromSeconds(30),
            };

            return http;
        }

        private async Task<PayPalAccessToken> GetPayPalAccessTokenAsync(HttpClient http)
        {
            var clientId = Configuration["Paypal:clientId"];
            var secret = Configuration["Paypal:Secret"];

            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes($"{clientId}:{secret}");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials"
            };

            request.Content = new FormUrlEncodedContent(form);

            HttpResponseMessage response = await http.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            PayPalAccessToken accessToken = JsonConvert.DeserializeObject<PayPalAccessToken>(content);
            return accessToken;
        }

        private async Task<PayPalPaymentCreatedResponse> CreatePaypalPaymentAsync(HttpClient http, PayPalAccessToken accessToken, double total, string currency)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "v1/payments/payment");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.access_token);

            var payment = JObject.FromObject(new
            {
                intent = "sale",
                redirect_urls = new
                {
                    return_url = Configuration["Paypal:returnURL"],
                    cancel_url = Configuration["Paypal:cancelURL"]
                },
                payer = new { payment_method = "paypal" },
                transactions = JArray.FromObject(new[]
                {
            new
            {
                amount = new
                {
                    total = total,
                    currency = currency
                }
            }
        })
            });

          request.Content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await http.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            PayPalPaymentCreatedResponse paypalPaymentCreated = JsonConvert.DeserializeObject<PayPalPaymentCreatedResponse>(content);
            return paypalPaymentCreated;
        }

        public async Task<PayPalPaymentExecutedResponse> executedPayment(string paymentId, string payerId)
        {
            try
            {
                HttpClient http = GetPaypalHttpClient();
                PayPalAccessToken accessToken = await GetPayPalAccessTokenAsync(http);

                return await ExecutePaypalPaymentAsync(http, accessToken, paymentId, payerId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "Failed to log in to paypal");
                return null; 
            }
            //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v1/payments/payment/{paymentId}/execute");
            //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.access_token);

            //    var payment = JObject.FromObject(new
            //    {
            //        payer_id = payerId
            //    });

            //    request.Content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

            //    HttpResponseMessage response = await http.SendAsync(request);
            //    string content = await response.Content.ReadAsStringAsync();
            //    PayPalPaymentExecutedResponse executedPayment = JsonConvert.DeserializeObject<PayPalPaymentExecutedResponse>(content);
            //    return executedPayment;
            //}
        }

        private async Task<PayPalPaymentExecutedResponse> ExecutePaypalPaymentAsync(HttpClient http, PayPalAccessToken accessToken, string paymentId, string payerId)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v1/payments/payment/{paymentId}/execute");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.access_token);
            var payment = JObject.FromObject(new
            {
                payer_id = payerId

            });
            request.Content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await http.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            PayPalPaymentExecutedResponse executedPayment = JsonConvert.DeserializeObject<PayPalPaymentExecutedResponse>(content);
            return executedPayment;
        }
    }
}
