using Microsoft.Extensions.Configuration;

namespace AuthTest.Mocks.Configurations
{
    public static class MockConfiguration
    {
        public static IConfiguration GetMockConfiguration()
        {
            var mockConfiguration = new Dictionary<string, string>
            {
                { "TokenOptions:Audience", "United-Backend" },
                { "TokenOptions:Issuer", "UZUMYMWKWKAPODSasda564213" },
                { "TokenOptions:AccessTokenExpiration", "10" },
                { "TokenOptions:SecurityKey", "yr.y(&6fYcv5u;JDFh+RK36N5HjS(Ur,t(KdX#E+hTOx!2J%gbqM;6AL@G5(#+53Pip$Y$xo(k2)2HnrW%rF1mVkHvN8O9wD.shl" },
                { "TokenOptions:RefreshTokenExpiration", "1440" },
                { "TokenOptions:RefreshTokenTTL", "180" },
                { "MailSettings:Server", "127.0.0.1" },

            };

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddInMemoryCollection(mockConfiguration);
            return configurationBuilder.Build();
        }
    }
}
