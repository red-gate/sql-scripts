using System.Reflection;

namespace RedGate.SSC.Windows.Web.Assets
{
    public static class AssetsAssembly
    {
        public static Assembly Get()
        {
            return typeof(AssetsAssembly).Assembly;
        }
    }
}
