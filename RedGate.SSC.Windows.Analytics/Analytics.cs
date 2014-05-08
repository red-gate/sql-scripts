using System.Linq;
using System.Reflection;
using Mixpanel.NET.Events;
using RedGate.SSC.Windows.Product;

namespace RedGate.SSC.Windows.Analytics
{
    internal class Analytics : IAnalytics
    {
        private readonly MixpanelTracker m_Tracker;

        internal Analytics()
        {
            m_Tracker = new MixpanelTracker(TheProduct.MixPanelToken);
        }

        public void Track(string name, object details)
        {
            PropertyInfo[] propertyInfos = details.GetType().GetProperties();

            var eventDetails = propertyInfos.ToDictionary(x => x.Name, x => x.GetValue(details, null));

            m_Tracker.Track(name, eventDetails);
        }
    }
}
