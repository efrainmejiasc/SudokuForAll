using Newtonsoft.Json;
using SudokuForAll.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SudokuForAll.Engine
{
    public class EnginePaypal : IEnginePaypal
    {

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
    }
}