// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityService
{
    //https://localhost:5005/.well-known/openid-configuration
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
             {
                 new IdentityResources.OpenId(),
                 new IdentityResources.Profile(),
                 new IdentityResource
                 {
                     Name = "role",
                     UserClaims = new List<string> { "role" }
                 }
             };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("PlaylistsAPI", "Playlists API"),
                new ApiScope("PlaylistService", "Playlist Service"),
                new ApiScope("email", "E-Mail")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("PlaylistsAPI")
                {
                    Scopes = new List<string>{ "PlaylistsAPI", "PlaylistService" },
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("ClientSecret1".Sha256())},
                    AllowedScopes = { "PlaylistsAPI", "PlaylistService", "email" }
                },
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = {new Secret("ClientSecret1".Sha256())},
                    ClientName = "Playlists from client App",
                    AllowedGrantTypes = GrantTypes.Code,
                    //RequireClientSecret = false,
                    //AllowRememberConsent = false,
                    RedirectUris = { "http://localhost:3000/signin-callback", "http://localhost:3000" },
                    FrontChannelLogoutUri = "http://localhost:3000/",
                    PostLogoutRedirectUris = { "http://localhost:3000/signout-callback" },
                    AllowedCorsOrigins = { "http://localhost:3000" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "PlaylistsAPI", "PlaylistService", "email"
                    },
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false,
                },
                new Client
                {
                    ClientId = "playlistsClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "PlaylistsAPI", "PlaylistService", "email" },
                },
                new Client
                {
                    ClientId = "playlists_web",
                    ClientName = "Playlists from client App",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "http://localhost:3000/signin-callback"// — this is client app port
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "http://localhost:3000/signout-callback"
                    },
                    AllowedCorsOrigins = { "http://localhost:3000" },
                    //ClientSecrets = new List<Secret>
                    //{
                    //    new Secret("secret".Sha256())
                    //},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "PlaylistsAPI", "PlaylistService", "email"
                    }
                }
            };
        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359–073C-434B-AD2D-A3932222DABE",
                    Username = "alice",
                    Password = "Pass123$",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith")
                    }
                }
            };
    }
}