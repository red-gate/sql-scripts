using Newtonsoft.Json;
using RedGate.SSC.Windows.Client.Chromium;

namespace RedGate.SSC.Windows.Client.JavaScriptModels
{
    public class ScriptItem : IBindableToJs
    {
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "SqlScript")]
        public string SqlScript { get; set; }

        [JsonProperty(PropertyName = "Authors")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "ContentItemId")]
        public int ContentItemId { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "CreatedDate")]
        public string CreatedDate { get; set; }

        [JsonProperty(PropertyName = "CategoryTag")]
        public string CategoryTag { get; set; }

        [JsonProperty(PropertyName = "Rating")]
        public int Rating { get; set; }

        [JsonProperty(PropertyName = "ViewsCount")]
        public int ViewsCount { get; set; }

        public string Name
        {
            get { return "RedGate_SSC_Windows_Client_ScriptItem"; }
        }
    }
}