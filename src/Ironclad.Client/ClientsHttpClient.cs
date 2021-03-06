﻿// Copyright (c) Lykke Corp.
// See the LICENSE file in the project root for more information.

namespace Ironclad.Client
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// An HTTP client for managing clients of the authorization server.
    /// </summary>
    public sealed class ClientsHttpClient : HttpClientBase, IClientsClient
    {
        private const string ApiPath = "/api/clients";

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientsHttpClient"/> class.
        /// </summary>
        /// <param name="authority">The authority.</param>
        /// <param name="innerHandler">The inner handler.</param>
        public ClientsHttpClient(string authority, HttpMessageHandler innerHandler = null)
            : base(authority, innerHandler)
        {
        }

        /// <summary>
        /// Gets the client summaries (or a subset thereof).
        /// </summary>
        /// <param name="startsWith">The start of the client identifier.</param>
        /// <param name="start">The zero-based start ordinal of the client set to return.</param>
        /// <param name="size">The total size of the client set.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The client summaries.</returns>
        public Task<ResourceSet<ClientSummary>> GetClientSummariesAsync(
            string startsWith = default,
            int start = 0,
            int size = 20,
            CancellationToken cancellationToken = default) =>
            this.GetAsync<ResourceSet<ClientSummary>>(
                this.RelativeUrl($"{ApiPath}?id={WebUtility.UrlEncode(startsWith)}&skip={NotNegative(start, nameof(start))}&take={NotNegative(size, nameof(size))}"),
                cancellationToken);

        /// <summary>
        /// Gets the specified client.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The client.</returns>
        public Task<Client> GetClientAsync(string clientId, CancellationToken cancellationToken = default) =>
            this.GetAsync<Client>(this.RelativeUrl($"{ApiPath}/{WebUtility.UrlEncode(NotNull(clientId, nameof(clientId)))}"), cancellationToken);

        /// <summary>
        /// Adds the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public Task AddClientAsync(Client client, CancellationToken cancellationToken = default) =>
            this.SendAsync<Client>(HttpMethod.Post, this.RelativeUrl(ApiPath), client, cancellationToken);

        /// <summary>
        /// Removes the specified client.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public Task RemoveClientAsync(string clientId, CancellationToken cancellationToken = default) =>
            this.DeleteAsync(this.RelativeUrl($"{ApiPath}/{WebUtility.UrlEncode(NotNull(clientId, nameof(clientId)))}"), cancellationToken);

        /// <summary>
        /// Modifies the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public Task ModifyClientAsync(Client client, CancellationToken cancellationToken = default) =>
            this.SendAsync<Client>(HttpMethod.Put, this.RelativeUrl($"{ApiPath}/{WebUtility.UrlEncode(NotNull(client?.Id, "client.Id"))}"), client, cancellationToken);
    }
}