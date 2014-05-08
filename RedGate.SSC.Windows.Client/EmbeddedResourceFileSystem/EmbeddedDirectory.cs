using System;
using System.IO;
using Microsoft.Owin.FileSystems;

namespace RedGate.SSC.Windows.Client.EmbeddedResourceFileSystem
{
    internal class EmbeddedDirectory : IFileInfo
    {
        private readonly string m_Name;

        public EmbeddedDirectory(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            m_Name = name;
        }

        public Stream CreateReadStream()
        {
            throw new InvalidOperationException();
        }

        public long Length
        {
            get { return 0; }
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
            get { return true; }
        }
    }
}