﻿// Copyright (c) Lykke Corp.
// See the LICENSE file in the project root for more information.

namespace Ironclad.Tests.Sdk
{
    using System.Net.Http;
    using Xunit;

    [Collection("Ironclad")]
    public class IntegrationTest : AuthenticationTest
    {
        private readonly IroncladFixture ironcladFixture;
        private readonly PostgresFixture postgresFixture;

        public IntegrationTest(AuthenticationFixture authenticationFixture, IroncladFixture ironcladFixture, PostgresFixture postgresFixture)
            : base(authenticationFixture)
        {
            this.ironcladFixture = ironcladFixture;
            this.postgresFixture = postgresFixture;
        }
    }
}