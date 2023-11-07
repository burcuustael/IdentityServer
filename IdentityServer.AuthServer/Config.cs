﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer.AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("resource_api1")
                {
                    Scopes={ "api1.read", "api1.write", "api1.update"},
                    ApiSecrets=new []{new Secret("secretapi1".Sha256()) }
                },
                new ApiResource("resource_api2")
                {
                    Scopes={ "api2.read", "api2.write", "api2.update" },
                    ApiSecrets=new []{new Secret("secretapi1".Sha256())}
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("api1.read","API 1 için okuma izni"),
                new ApiScope("api1.write","API 1 için yazma izni"),
                new ApiScope("api1.update","API 1 için güncelleme izni"),

                new ApiScope("api2.read","API 2 için okuma izni"),
                new ApiScope("api2.write","API 2 için yazma izni"),
                new ApiScope("api2.update","API 2 için güncelleme izni"),

            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>() {
            new Client()
            {
                ClientId = "Client1",
                ClientName = "Client 1 web site uyg.",
                ClientSecrets = new[]{new Secret("secret".Sha256())},
                AllowedGrantTypes=GrantTypes.ClientCredentials,
                AllowedScopes={"api1.read"}
            },
            new Client()
            {
                ClientId = "Client2",
                ClientName = "Client 2 web site uyg.",
                ClientSecrets = new[]{new Secret("secret".Sha256())},
                AllowedGrantTypes=GrantTypes.ClientCredentials,
                AllowedScopes={"api1.read","api2.write","api2.update"}
            },

            new Client()
            {
                ClientId="Client1-Mvc",
                ClientName="Client1 app mvc uygulaması",
                ClientSecrets = new[]{new Secret("secret".Sha256())},
                AllowedGrantTypes=GrantTypes.Hybrid,
                RedirectUris=new List<string>{"https://localhost:7265/sign-oidc"},
                AllowedScopes={IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile}    
            }
        };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>()
            { 
                new TestUser{SubjectId="1",Username="burcus",Password="password",Claims=new List<Claim>(){
                new Claim("given_name","Burcu"),
                new Claim("family_name","Ustael")}
                },

                 new TestUser{SubjectId="2",Username="dora",Password="password",Claims=new List<Claim>(){
                new Claim("given_name","Burak"),
                new Claim("family_name","Ustael")}
                }
            };
        }
    }
}
