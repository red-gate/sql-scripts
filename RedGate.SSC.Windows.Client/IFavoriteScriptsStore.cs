using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace RedGate.SSC.Windows.Client
{
    internal interface IFavoriteScriptsStore
    {
        IEnumerable<FavoriteScript> Favorites { get; }

        void AddOrUpdateFavoriteDate(int favoriteId);
    }
}