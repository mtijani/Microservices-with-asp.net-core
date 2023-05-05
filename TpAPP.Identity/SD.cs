using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpAPP.Services.Identity
{
    public static  class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> { 
                new ApiScope("TpAPP", "TpAPP Server "),
                new ApiScope(name:"read",displayName:"Read your data"),
                new ApiScope(name:"write",displayName:"write your data"),
                new ApiScope(name:"delete",displayName:"delete your data"),

            };
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "read","write","profile"
                    }

                },
                 new Client
                {
                    ClientId="TpAPP",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris={ "https://localhost:44319/signin-oidc" },
                    PostLogoutRedirectUris={ "https://localhost:44319/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                     {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "TpAPP"



                     }


                },

            };
    }
}
