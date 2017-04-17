using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Xbox.Services.UnitTests.TitleStorage
{
    using global::System;
    using global::System.Net;
    using global::System.Threading.Tasks;

    using Microsoft.Xbox.Services.TitleStorage;

    using Shared.TitleStorage;

    using Moq;

    [TestClass]
    public class TitleStorageTestsUsingMoq
    {
        [TestMethod]
        public async Task MoqTestGetQuotaAsyncWithGlobalStorage()
        {
            // Arrange
            var appConfig = XboxLiveAppConfiguration.Load();
            var mockHttpHandler = new Mock<IHttpRequestHandler>();
            var user = new XboxLiveUser("xuid", "gamertag");
            var xboxLiveHttpRequest = XboxLiveHttpRequest.Create(
                "GET", 
                TitleStorageService.TitleStorageBaseUri.AbsoluteUri, 
                string.Format("global/scids/{0}", appConfig.PrimaryServiceConfigId));

            mockHttpHandler.Setup(
                x => x.Create(
                    "GET", 
                    TitleStorageService.TitleStorageBaseUri.AbsoluteUri, $"global/scids/{appConfig.PrimaryServiceConfigId}"))
                    .Returns(xboxLiveHttpRequest).Verifiable();
            var titleStorageService = new TitleStorageService(mockHttpHandler.Object, appConfig);


            var httpLiveResponse = new XboxLiveHttpResponse();
            var mockHttpResponse = new Mock<HttpWebResponse>();
            httpLiveResponse.response = mockHttpResponse.Object;
            var expectedQuotaInfo = new TitleStorageQuota { QuotaBytes = 16777216, UsedBytes = 619 };
            httpLiveResponse.ResponseBodyString = JsonSerialization.ToJson( new QuotaInfoResult  { QuotaInfo = expectedQuotaInfo });
            mockHttpHandler.Setup(
                x => x.GetResponseWithAuth(
                    xboxLiveHttpRequest,
                    It.IsAny<XboxLiveUser>())).
               Returns(Task.FromResult(httpLiveResponse)).Verifiable();

            // Act
            var resultQuotaInfo = await titleStorageService.GetQuotaAsync(user, TitleStorageType.GlobalStorage);

            // Assert
            Assert.AreEqual(TitleStorageType.GlobalStorage, resultQuotaInfo.StorageType);
            Assert.AreEqual(expectedQuotaInfo.QuotaBytes, resultQuotaInfo.QuotaBytes);
            Assert.AreEqual(expectedQuotaInfo.UsedBytes, resultQuotaInfo.UsedBytes);
        }

        [TestMethod]
        public async Task MoqTestGetQuotaAsyncWithTrustedPlatform()
        {
            // Arrange
            var appConfig = XboxLiveAppConfiguration.Load();
            var mockHttpHandler = new Mock<IHttpRequestHandler>();
            var user = new XboxLiveUser("xuid", "gamertag");
            var xboxLiveHttpRequest = XboxLiveHttpRequest.Create(
                "GET",
                TitleStorageService.TitleStorageBaseUri.AbsoluteUri,
                string.Format("global/scids/{0}", appConfig.PrimaryServiceConfigId));

            mockHttpHandler.Setup(
                x => x.Create(
                    "GET",
                    TitleStorageService.TitleStorageBaseUri.AbsoluteUri, $"global/scids/{appConfig.PrimaryServiceConfigId}"))
                    .Returns(xboxLiveHttpRequest).Verifiable();
            var titleStorageService = new TitleStorageService(mockHttpHandler.Object, appConfig);


            var httpLiveResponse = new XboxLiveHttpResponse();
            var mockHttpResponse = new Mock<HttpWebResponse>();
            httpLiveResponse.response = mockHttpResponse.Object;
            var expectedQuotaInfo = new TitleStorageQuota { QuotaBytes = 16777216, UsedBytes = 619 };
            httpLiveResponse.ResponseBodyString = JsonSerialization.ToJson(new QuotaInfoResult { QuotaInfo = expectedQuotaInfo });
            mockHttpHandler.Setup(
                x => x.GetResponseWithAuth(
                    xboxLiveHttpRequest,
                    It.IsAny<XboxLiveUser>())).
               Returns(Task.FromResult(httpLiveResponse)).Verifiable();

            // Act
            var resultQuotaInfo = await titleStorageService.GetQuotaAsync(user, TitleStorageType.GlobalStorage);

            // Assert
            Assert.AreEqual(TitleStorageType.GlobalStorage, resultQuotaInfo.StorageType);
            Assert.AreEqual(expectedQuotaInfo.QuotaBytes, resultQuotaInfo.QuotaBytes);
            Assert.AreEqual(expectedQuotaInfo.UsedBytes, resultQuotaInfo.UsedBytes);
        }
    }
}
