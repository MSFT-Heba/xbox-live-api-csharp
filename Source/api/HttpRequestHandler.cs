using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xbox.Services.Shared.TitleStorage
{
    using global::System.Threading.Tasks;

    class HttpRequestHandler: IHttpRequestHandler
    {
        public XboxLiveHttpRequest Create(string httpMethod, string serverName, string pathQueryFragment)
        {
            return new XboxLiveHttpRequest(httpMethod, serverName, pathQueryFragment);
        }

        public void SetRangeHeader(XboxLiveHttpRequest xboxLiveHttpRequest, uint startByte, uint endByte)
        {
            xboxLiveHttpRequest.SetRangeHeader(startByte, endByte);
        }

        public void SetCustomHeader(XboxLiveHttpRequest xboxLiveHttpRequest, string headerName, string headerValue)
        {
            xboxLiveHttpRequest.SetCustomHeader(headerName, headerValue);
        }

        public Task<XboxLiveHttpResponse> GetResponseWithAuth(XboxLiveHttpRequest xboxLiveHttpRequest, XboxLiveUser user)
        {
            return xboxLiveHttpRequest.GetResponseWithAuth(user);
        }
    }
}
