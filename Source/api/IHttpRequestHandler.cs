using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xbox.Services.Shared.TitleStorage
{
    using global::System.Threading.Tasks;

    public interface IHttpRequestHandler
    {
        XboxLiveHttpRequest Create(string httpMethod, string serverName, string pathQueryFragment);

        void SetRangeHeader(XboxLiveHttpRequest xboxLiveHttpRequest, uint startByte, uint endByte);

        void SetCustomHeader(XboxLiveHttpRequest xboxLiveHttpRequest, string headerName, string headerValue);

        Task<XboxLiveHttpResponse> GetResponseWithAuth(XboxLiveHttpRequest xboxLiveHttpRequest, XboxLiveUser user);
    }
}
