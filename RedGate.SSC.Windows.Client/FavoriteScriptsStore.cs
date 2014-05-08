using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using log4net;
using Newtonsoft.Json;
using RedGate.SSC.Windows.Product;

namespace RedGate.SSC.Windows.Client
{
    [UsedImplicitly]
    internal class FavoriteScriptsStore : IFavoriteScriptsStore
    {
        private static readonly ILog s_Logger = ObjectFactory.Get<ILog>();

        private readonly Dictionary<int, string> m_Store = new Dictionary<int, string>();
 
        public FavoriteScriptsStore()
        {
            if (File.Exists(DataFile))
            {
                m_Store = DeserializeStore();
            }
        }

        public IEnumerable<FavoriteScript> Favorites
        {
            get { return m_Store.Select(x => new FavoriteScript(x.Key, x.Value)); }
        }

        public void AddOrUpdateFavoriteDate(int favoriteId)
        {
            //From Source\Concrete\DataModel\BriefcaseEntry.cs in SQL Server Central source, they use UTC now
            m_Store[favoriteId] = DateTime.UtcNow.ToString("yyyy-MM-dd");

            try
            {
                File.WriteAllText(DataFile, JsonConvert.SerializeObject(m_Store, Formatting.Indented));
            }
            catch (Exception e)
            {
                s_Logger.Error("Unable to serialize the store to json", e);
            }
        }

        private Dictionary<int, string> DeserializeStore()
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<int, string>>(File.ReadAllText(DataFile));
            }
            catch (Exception e)
            {
                s_Logger.Error("Unable to read favorite store", e);
                
                return new Dictionary<int, string>();
            }
        }

        private string DataFile
        {
            get { return Path.Combine(TheProduct.ProductApplicationData, "favorite-scripts.json"); }
        }
    }
}