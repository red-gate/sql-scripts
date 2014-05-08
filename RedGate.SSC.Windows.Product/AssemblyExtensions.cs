using System;
using System.Linq;
using System.Reflection;

namespace RedGate.SSC.Windows.Product
{
    internal static class AssemblyExtensions
    {
        internal static T GetCustomAttribute<T>(this Assembly assembly)
            where T : Attribute
        {
            return (T)assembly.GetCustomAttributes(typeof(T), true).Single();
        }
    }
}