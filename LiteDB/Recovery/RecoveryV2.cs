using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LiteDB.Engine.FileReader;

namespace LiteDB
{
    public partial class LiteEngine
    {
        /// <summary>
        /// Attempts to recover a LiteDB database with one or more broken collections.
        /// </summary>
        /// <returns>A list of names of unrecoverable collection.</returns>
        /// <exception cref="ArgumentNullException">Gets thrown when the filename/path is null, empty or whitespace</exception>
        /// <exception cref="FileNotFoundException">Gets thrown when the file specified by the filename/path could not be found</exception>
        /// <exception cref="LiteException">Gets thrown when another LiteDB specific error occurs</exception>
        public static IList<string> RecoveryV2(string filename, string password = null)
        {
            if (filename.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(filename));
            }

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException();
            }

            IList<string> unrecoverableCollections = new List<string>();

            var tempFilename = FileHelper.GetTempFile(filename, "-temp", true);

            using (var oldDbStream = new FileStream(filename, System.IO.FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[BasePage.PAGE_SIZE];

                // read headerPage
                oldDbStream.Read(buffer, 0, buffer.Length);

                // checks if plain or encrypted
                IFileReader reader;
                if (Encoding.UTF8.GetString(buffer, 25, HeaderPage.HEADER_INFO.Length) == HeaderPage.HEADER_INFO && buffer[52] == 7)
                {
                    reader = new FileReaderV7(oldDbStream, password);
                }
                else
                {
                    throw new LiteException(0, "Invalid data file format to upgrade");
                }

                try
                {
                    using (var engine = new LiteEngine(tempFilename, password))
                    {
                        unrecoverableCollections = engine.Rebuild(reader);
                    }
                }
                finally
                {
                    reader.Dispose();
                }
            }

            // delete source file so the fixed file can take its place
            File.Delete(filename);

            // rename temp file into filename
            File.Move(tempFilename, filename);

            return unrecoverableCollections;
        }
    }
}