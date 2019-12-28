﻿namespace Hqub.MusicBrainz.API.Services
{
    using Hqub.MusicBrainz.API.Entities;
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System;
    using System.Threading.Tasks;

    public class ArtistService
    {
        MusicBrainzClient client;

        public ArtistService(MusicBrainzClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Lookup an artist in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The artist MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public async Task<Artist> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = client.CreateLookupUrl(Artist.EntityName, id, inc);

            return await client.GetAsync<Artist>(url);
        }

        /// <summary>
        /// Search for an artist in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of artists to return (default = 25).</param>
        /// <param name="offset">The offset to the artists list (enables paging, default = 0).</param>
        /// <returns></returns>
        public async Task<ArtistList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            string url = client.CreateSearchTemplate(Artist.EntityName, query, limit, offset);

            return await client.GetAsync<ArtistList>(url);
        }

        /// <summary>
        /// Search for an artist in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of artists to return (default = 25).</param>
        /// <param name="offset">The offset to the artists list (enables paging, default = 0).</param>
        /// <returns></returns>
        public async Task<ArtistList> SearchAsync(QueryParameters<Artist> query, int limit = 25, int offset = 0)
        {
            return await SearchAsync(query.ToString(), limit, offset);
        }

        /// <summary>
        /// Browse all the artists in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of artists to return (default = 25).</param>
        /// <param name="offset">The offset to the artists list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public async Task<ArtistList> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            string url = client.CreateBrowseTemplate(Artist.EntityName, entity, id, limit, offset, inc);

            return await client.GetAsync<ArtistList>(url);
        }
    }
}
