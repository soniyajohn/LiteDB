using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB.Engine.FileReader;

namespace LiteDB
{
    public partial class LiteEngine
    {
        /// <summary>
        /// Fill current database with data inside file reader
        /// </summary>
        /// <exception cref="Exception">Might throw an exception if something goes wrong while trying to read the collectionNames from the DB header.</exception>
        internal IList<string> Rebuild(IFileReader reader)
        {
            var unrecoverableCollections = new HashSet<string>();
            foreach (var collection in reader.GetCollections())
            {
                var oldDocumentCount = reader.GetDocumentCountOfCollection(collection);
                try
                {
                    // first create all user indexes (exclude _id index)
                    foreach (var index in reader.GetIndexes(collection))
                    {
                        EnsureIndex(collection, index.Field, index.Expression, index.Unique);
                    }
                }
                catch
                {
                    // Silent ignore, we can still create the indexes on the new db afterwards.
                }

                try
                {
                    // get all documents from current collection
                    var docs = reader.GetDocuments(collection).ToList();

                    // and insert into
                    var insertCount = this.Insert(collection, docs, BsonType.ObjectId);
                    if (oldDocumentCount != insertCount)
                    {
                        unrecoverableCollections.Add(collection);
                    }
                }
                catch
                {
                    // Silent ignore, sadly couldn't recover data.
                    unrecoverableCollections.Add(collection);
                }
            }

            return unrecoverableCollections.ToList();
        }
    }
}