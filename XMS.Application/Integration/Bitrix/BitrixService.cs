using Microsoft.Extensions.Configuration;
using XMS.Application.Abstractions.Integration.Services;

namespace XMS.Application.Integration.Bitrix;


internal class BitrixService(BitrixClient bitrixClient, IConfiguration configuration) : IBitrixService
{
    public async Task<BitrixUser?> GetUserAsync(string userName, string password)
    {
        var bitrixAuthParams = new Dictionary<string, string>
        {
            ["USER_LOGIN"] = userName,
            ["USER_PASSWORD"] = password,
            ["AUTH_FORM"] = "Y",
            ["TYPE"] = "AUTH"
        };

        var httpContent = new FormUrlEncodedContent(bitrixAuthParams);

        var authUri = configuration["BitrixClientConfig:AuthUri"];

        var authResponse = await bitrixClient.PostDataAsync<BitrixAuthResponse>(authUri, httpContent);

        var bitrixUser = authResponse?.User;

        return bitrixUser;
    }
}
