using System;
using System.IO;
using Microsoft.Owin.FileSystems;

namespace RedGate.SSC.Windows.Client.EmbeddedResourceFileSystem
{
    internal class EmbeddedFile : IFileInfo
    {
        private readonly string m_Name;
        private readonly Lazy<Stream> m_Bytes;

        public EmbeddedFile(string name, Lazy<Stream> bytes)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (bytes == null)
                throw new ArgumentNullException("bytes");

            m_Name = name;
            m_Bytes = bytes;
        }

        public Stream CreateReadStream()
        {
            return m_Bytes.Value;
        }

        public long Length
        {
            get { return m_Bytes.Value.Length; }
        }

        public string PhysicalPath
        {
            get { return null; }
        }

        public string Name
        {
            get { return m_Name; }
        }

        public DateTime LastModified
        {
            get { return DateTime.UtcNow; }
        }

        public bool IsDirectory
        {
            get { return false; }
        }
    }
}