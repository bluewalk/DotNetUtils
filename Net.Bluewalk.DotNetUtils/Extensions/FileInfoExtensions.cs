using System.IO;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Determines whether a file is locked by the OS.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns><c>true</c> if locked; otherwise, <c>false</c>.</returns>
        public static bool IsLocked(this FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                stream?.Close();
            }

            //file is not locked
            return false;
        }
    }
}
