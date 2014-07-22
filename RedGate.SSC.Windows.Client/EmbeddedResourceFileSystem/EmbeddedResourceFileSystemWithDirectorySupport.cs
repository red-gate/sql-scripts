using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Owin.FileSystems;

namespace RedGate.SSC.Windows.Client.EmbeddedResourceFileSystem
{
    /// <summary>
    /// This tries to map all embedded resources back to a nest structure as it would have existed
    /// on the file system. Very naive, assumes all '.' characters are Directory separators, except
    /// for final which will become the file extension separator
    /// </summary>
    internal class EmbeddedResourceFileSystemWithDirectorySupport : IFileSystem
    {
        private readonly Assembly m_Asm;

        public EmbeddedResourceFileSystemWithDirectorySupport(Assembly asm)
        {
            m_Asm = asm;
        }

        public bool TryGetFileInfo(string subpath, out IFileInfo fileInfo)
        {
            fileInfo = null;
            var resourcesAsNestedFiles = GetResourcesAsNestedFiles();

            var resourceToServe = resourcesAsNestedFiles.SingleOrDefault(x => subpath == "/" + x.Key);

            if (resourceToServe.Key == null) //TODO: This is basically because the wwwroot isn't set properly. Everything or nothing should be in 'dist'
                resourceToServe = resourcesAsNestedFiles.SingleOrDefault(pair => "/dist" + subpath == "/" + pair.Key);

            if (resourceToServe.Key != null)
            {
                fileInfo = new EmbeddedFile(resourceToServe.Key, resourceToServe.Value);
            }

            return resourceToServe.Key != null;
        }

        private string ConvertResourceNameToUriLocalPath(string manifest)
        {
            string withoutNameSpace = manifest.Replace(m_Asm.GetName().Name + ".", "");
            string periodReplacedWithForwardSlash = withoutNameSpace.Replace(".", "/");
            var finalSlashWhereFileExtensionWillGo = periodReplacedWithForwardSlash.LastIndexOf('/');
            return periodReplacedWithForwardSlash.Insert(finalSlashWhereFileExtensionWillGo, ".").Remove(finalSlashWhereFileExtensionWillGo + 1, 1);
        }

        private Dictionary<string, Lazy<Stream>> GetResourcesAsNestedFiles()
        {
            return m_Asm.GetManifestResourceNames().ToDictionary(ConvertResourceNameToUriLocalPath, s => new Lazy<Stream>(() => m_Asm.GetManifestResourceStream(s)));
        }

        /// <summary>
        /// This isn't actually used by the production code, as Directory browsing isn't used in the app
        /// </summary>
        public bool TryGetDirectoryContents(string subpath, out IEnumerable<IFileInfo> contents)
        {
#if DEBUG
            contents = GetResourcesAsNestedFiles()
                .Where(x => ("/" + x.Key).StartsWith(subpath))
                .GroupBy(x => x.Key.Split('/').ToArray()[subpath.Count(c => c == '/') - 1])
                .ToDictionary(x => x.Key, x => x.ToList())
                .Select(x => Path.HasExtension(x.Key) ? (IFileInfo)new EmbeddedFile(x.Key, x.Value.Single().Value) : new EmbeddedDirectory(x.Key))
                .ToList();

            return contents.Any();
#else
            contents = null;
            return false;
#endif
        }
    }
}