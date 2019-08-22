using PayPal.Api;
using SudokuForAll.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuForAll.Engine
{
    public interface IEnginePaypal
    {
        Task<RespuestaPaypalToken> GetTokenPaypal();
        APIContext GetApiContext(string accessToken);
    }
}
