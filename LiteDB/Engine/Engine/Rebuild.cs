using System;
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
        internal void Rebuild(IFileReader reader)
        {
            foreach (var collection in reader.GetCollections())
            {
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
                    this.Insert(collection, docs, BsonType.ObjectId);
                }
                catch
                {
                    // Silent ignore, sadly couldn't recover data.
                }
            }
        }
    }
}