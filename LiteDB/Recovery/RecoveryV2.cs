using System;
using System.IO;
using System.Text;
using LiteDB.Engine.FileReader;

namespace LiteDB
{
    public partial class LiteEngine
    {
        /// <summary>
        /// Attempts to recover a LiteDB database with one or more broken collections. Returns true if the recovery process finished successfully.
        /// Returns false if the file could not be found.
        /// <exception cref="ArgumentNullException">Gets thrown when the filename/path is null, empty or whitespace</exception>
        /// <exception cref="LiteException">Gets thrown when another LiteDB specific error occurs</exception>
        /// </summary>
        public static bool RecoveryV2(string filename, string password = null)
        {
            if (filename.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(filename));
            }

            if (!File.Exists(filename))
            {
                return false;
            }

            var backup = FileHelper.GetTempFile(filename, "-backup", true);

            var tempFilename = FileHelper.GetTempFile(filename, "-temp", true);

            using (var stream = new FileStream(filename, System.IO.FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[BasePage.PAGE_SIZE];

                // read headerPage
                stream.Read(buffer, 0, buffer.Length);

                // checks if plain or encrypted
                IFileReader reader;
                if (Encoding.UTF8.GetString(buffer, 25, HeaderPage.HEADER_INFO.Length) == HeaderPage.HEADER_INFO && buffer[52] == 7)
                {
                    reader = new FileReaderV7(stream, password);
                }
                else
                {
                    throw new LiteException(0, "Invalid data file format to upgrade");
                }

                try
                {
                    using (var engine = new LiteEngine(tempFilename, password))
                    {
                        engine.Rebuild(reader);
                    }
                }
                finally
                {
                    reader.Dispose();
                }
            }

            // rename source filename to backup name
            File.Move(filename, backup);

            // rename temp file into filename
            File.Move(tempFilename, filename);

            return true;
        }
    }
}