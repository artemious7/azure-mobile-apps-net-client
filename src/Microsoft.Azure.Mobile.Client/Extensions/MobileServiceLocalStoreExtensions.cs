﻿// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using Microsoft.WindowsAzure.MobileServices.Query;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.MobileServices.Sync
{
    /// <summary>
    ///  Provides extension methods on <see cref="IMobileServiceLocalStore"/>.
    /// </summary>
    internal static class MobileServiceLocalStoreExtensions
    {
        /// <summary>
        /// Updates or inserts data in local table.
        /// </summary>
        /// <param name="store">Instance of <see cref="IMobileServiceLocalStore"/></param>
        /// <param name="tableName">Name of the local table.</param>
        /// <param name="item">Item to be inserted.</param>
        /// <param name="fromServer"><code>true</code> if the call is made based on data coming from the server e.g. in a pull operation; <code>false</code> if the call is made by the client, such as insert or update calls on an <see cref="IMobileServiceSyncTable"/>.</param>
        /// <returns>A task that completes when item has been upserted in local table.</returns>
        public static Task UpsertAsync(this IMobileServiceLocalStore store, string tableName, ITable item, bool fromServer)
            => store.UpsertAsync(tableName, new[] { item }, fromServer);

        /// <summary>
        /// Deletes an item with the specified id in the local table.
        /// </summary>
        /// <param name="store">Instance of <see cref="IMobileServiceLocalStore"/></param>
        /// <param name="tableName">Name of the local table.</param>
        /// <param name="id">Id for the object to be deleted.</param>
        /// <returns>A task that compltes when delete has been executed on local table.</returns>
        public static Task DeleteAsync(this IMobileServiceLocalStore store, string tableName, string id)
            => store.DeleteAsync(tableName, new[] { id });

        /// <summary>
        /// Counts all the items in a local table
        /// </summary>
        /// <param name="store">Instance of <see cref="IMobileServiceLocalStore"/></param>
        /// <param name="tableName">Name of the table</param>
        /// <returns>Task that will complete with count of items.</returns>
        public async static Task<long> CountAsync(this IMobileServiceLocalStore store, string tableName)
        {
            var query = new MobileServiceTableQueryDescription(MobileServiceLocalSystemTables.OperationQueue);
            return await CountAsync(store, query);
        }

        /// <summary>
        /// Counts all the items returned from the query
        /// </summary>
        /// <param name="store">An instance of <see cref="IMobileServiceLocalStore"/></param>
        /// <param name="query">An instance of <see cref="MobileServiceTableQueryDescription"/></param>
        /// <returns>Task that will complete with count of items.</returns>
        public static async Task<long> CountAsync(this IMobileServiceLocalStore store, MobileServiceTableQueryDescription query)
        {
            query.Top = 0;
            query.IncludeTotalCount = true;
            var result = await store.QueryAsync(query);
            //TODO ADD result
            //return result.TotalCount;
            return 0;
        }

        /// <summary>
        /// Executes the query on local store and returns the parsed result
        /// </summary>
        /// <param name="store">An instance of <see cref="IMobileServiceLocalStore"/></param>
        /// <param name="query">An instance of <see cref="MobileServiceTableQueryDescription"/></param>
        /// <returns>Task that will complete with the parsed result of the query.</returns>
        public static async Task<ITable> QueryAsync(this IMobileServiceLocalStore store, MobileServiceTableQueryDescription query)
            => await store.ReadAsync(query);

        /// <summary>
        /// Executes the query on local store and returns the first or default item from parsed result
        /// </summary>
        /// <param name="store">An instance of <see cref="IMobileServiceLocalStore"/></param>
        /// <param name="query">An instance of <see cref="MobileServiceTableQueryDescription"/></param>
        /// <returns>Task that will complete with the first or default item from parsed result of the query.</returns>
        public static async Task<ITable> FirstOrDefault(this IMobileServiceLocalStore store, MobileServiceTableQueryDescription query)
        {
            var result = await store.QueryAsync(query);
            return result;
        }
    }
}
