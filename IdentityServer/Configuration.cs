
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "rc.scope",
                    UserClaims =
                    {
                        "rc.garndma",
                        "role"
                    }
                }
            };

        public static IEnumerable<ApiScope> GetScopes() =>
        new List<ApiScope>
        {
            //new ApiScope("rc.api",new string[]{"rc.api.grandma"})
            new ApiScope("Scope1",new string[]{"rc.api.garndma","id"}),
            new ApiScope("Scope2"),
            new ApiScope("rc.api")
        };

        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource>
            {
                new ApiResource("ApiOne","Api One")
                {
                    Scopes = { "Scope1" }
                },
                new ApiResource("ApiTwo")
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client {
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    RedirectUris = { "https://localhost:7109/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7109/Home/Index" },

                    AllowedScopes = {
                        "Scope1",
                        "Scope2",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rc.scope",
                    },
                    // puts all the claims in the id token
                    //AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    RequireConsent = false,
                },
                new Client
                {
                    ClientId = "client_id_js",

                    AllowedGrantTypes = GrantTypes.Code,
                    //RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "https://localhost:7270/home/signin" },
                    PostLogoutRedirectUris = { "https://localhost:7270/Home/Index" },
                    AllowedCorsOrigins = { "https://localhost:7270" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "Scope1",
                        "Scope2",
                        "rc.scope",
                    },

                    //AccessTokenLifetime = 1,

                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                }
            };
    }
}
