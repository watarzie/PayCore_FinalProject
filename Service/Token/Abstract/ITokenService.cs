using Base.Response;
using Base.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Token.Abstract
{
    public interface ITokenService
    {
        BaseResponse<TokenResponse> GenerateToken(TokenRequest tokenRequest);
    }
}
