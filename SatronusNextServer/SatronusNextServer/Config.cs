using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatronusNextServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResource()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("SatronusNextResourceScope", new []{"role", "admin", "user", "SatronusNextResource", "SatronusNextResource.admin", "SatronusNextResource.user" })
            };
        }
        public static IEnumerable<ApiResource> GetApiResource()
        {
            return new List<ApiResource>
            {
                new ApiResource("SatronusNextResource")
                {
                    /*ApiSecrets =
                    {
                        new Secret("SatronusNextResource".Sha256())
                    },*/
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "SatronusNextResourceScope",
                            DisplayName = "Scope for the SatronusNextResource ApiResource"
                        }
                    },
                    UserClaims = {"role", "admin", "user", "SatronusNextResource", "SatronusNextResource.admin", "SatronusNextResource.user" }
                }
            };
        }
        public static IEnumerable<Client> GetClient()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "StachronusNextClient",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 120,
                    IdentityTokenLifetime = 120,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    SlidingRefreshTokenLifetime = 30,
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AlwaysSendClientClaims = true,
                    Enabled = true,
                    ClientSecrets = new List<Secret> { new Secret("StachronusNextClient".Sha256())},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "SatronusNextResource"
                    }
                }
            };
        }
    }
}
