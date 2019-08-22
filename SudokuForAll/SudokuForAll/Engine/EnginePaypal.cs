using Newtonsoft.Json;
using PayPal.Api;
using SudokuForAll.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace SudokuForAll.Engine
{
    public class EnginePaypal : IEnginePaypal
    {
        public Dictionary <string, string> GetConfig()
        {
            return new Dictionary<string, string>() {
                { "clientId", EngineData.ClientId},
                { "clientSecret", EngineData.KeySecret }
            };
        }

        public async Task<RespuestaPaypalToken> GetTokenPaypal()
        {
            string respuesta = string.Empty;
            RespuestaPaypalToken R = new RespuestaPaypalToken();
            using (HttpClient client = new HttpClient())
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(EngineData.ClientId + ":" + EngineData.KeySecret);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                Uri url = new Uri(EngineData.EndPointTokenPaypal, UriKind.Absolute);
                List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();
                formData.Add(new KeyValuePair<string, string>(EngineData.Grant_Type, EngineData.Client_Credentials));
                HttpContent content = new FormUrlEncodedContent(formData);
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    R = JsonConvert.DeserializeObject<RespuestaPaypalToken>(respuesta);
                }
            }
            return R;
        }

        public APIContext GetApiContext(string accessToken)
        {
            APIContext  apiContext = new APIContext(accessToken);
            apiContext.Config = GetConfig();
            return apiContext;
        }

        public Payment CreatePayment(string baseUrl, string intent,string accesToken)
        {
            // Contexto de la API, Pase un objeto `APIContext` para autenticar la llamada y para enviar una identificación de solicitud única (que asegura la idempotencia). 
            // El SDK genera un id de solicitud si no pasa uno explícitamente.

            APIContext apiContext = GetApiContext(accesToken);
            Payment payment = new Payment()
            {
                intent = intent,    // 'SALE' or `authorize`
                payer = new Payer() { payment_method = "paypal" },
                transactions = GetTransactionsList(),
                redirect_urls = GetReturnUrls(baseUrl, intent)
            };
            //Crear pago usando un APICONTEX Valido
            var createdPayment = payment.Create(apiContext);

            return createdPayment;
        }


        private List<Transaction> GetTransactionsList()
        {
            // Una Transaccion define para quien es el pago y quien lo realiza 
            var transactionList = new List<Transaction>();

            // La creacion del pago requiere una lista de transacciones  
            // Agregar una transaccion a la lista 
            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = "3000",//NUMERO
                amount = new Amount()
                {
                    currency = "USD",
                    total = "100.00",       
                    details = new Details() 
                    {
                        tax = "15",
                        shipping = "10",
                        subtotal = "75"
                    }
                },

                item_list = new ItemList()
                {
                    items = new List<Item>()
                    {
                        new Item()
                        {
                         name = "Curso",
                         currency = "USD",
                         price = "15",
                         quantity = "5",
                         sku = "sku"
                        }
                    }
                }
            });
            return transactionList;
        }

        public Payment ExecutePayment(string paymentId, string payerId,string accesToken)
        {
            var apiContext = GetApiContext(accesToken);

            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };
            var executedPayment = payment.Execute(apiContext, paymentExecution);

            return executedPayment;
        }

        private RedirectUrls GetReturnUrls(string baseUrl, string intent)
        {
            var returnUrl = intent == "sale" ? "/Home/PaymentSuccessful" : "/Home/AuthorizeSuccessful";

            return new RedirectUrls()
            {
                cancel_url = baseUrl + "/Home/PaymentCancelled",
                return_url = baseUrl + returnUrl
            };
        }

     }
}