using System;
using System.Drawing;

namespace RedGate.SSC.Windows.Update.Interfaces
{
    public interface ICheckForUpdateServiceFactory
    {
        ICheckForUpdatesService Create(string displayName, Version productVersion, Icon productIcon);
    }
}