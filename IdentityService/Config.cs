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
                 new IdentityResources.Profile()
             };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("playlistsAPI", "Playlists API"),
                new ApiScope("PlaylistService", "Playlist Service")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "playlistsClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "playlistsAPI", "PlaylistService" }
                },
                new Client
                {
                    ClientId = "playlists_web",
                    ClientName = "Playlists from client App",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:3000/signin-oidc"// — this is client app port
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:3000/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,"playlistsAPI", "PlaylistService"
                    }
                }
            };
        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359–073C-434B-AD2D-A3932222DABE",
                    Username = "mehmet",
                    Password = "mehmet",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "mehmet"),
                        new Claim(JwtClaimTypes.FamilyName, "ozkaya")
                    }
                }
            };
    }
}