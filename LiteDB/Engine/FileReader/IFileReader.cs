using System;
using System.Collections.Generic;

namespace LiteDB.Engine.FileReader
{
    /// <summary>
    /// Interface to read current or old datafile structure - Used to shirnk/upgrade datafile from old LiteDB versions
    /// </summary>
    interface IFileReader : IDisposable
    {
        int UserVersion { get; }

        /// <summary>
        /// Get all collections name from database
        /// </summary>
        IEnumerable<string> GetCollections();

        /// <summary>
        /// Get total itemcount of specified collection
        /// </summary>
        long GetDocumentCountOfCollection(string collection);

        /// <summary>
        /// Get all indexes from collection (except _id index)
        /// </summary>
        IEnumerable<IndexInfo> GetIndexes(string name);

        /// <summary>
        /// Get all documents from a collection
        /// </summary>
        IEnumerable<BsonDocument> GetDocuments(string collection);
    }
}