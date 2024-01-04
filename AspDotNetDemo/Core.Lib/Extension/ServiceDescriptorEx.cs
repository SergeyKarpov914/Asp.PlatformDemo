using Microsoft.Extensions.DependencyInjection;

namespace Clio.Demo.Core.Extension
{
    public static class ServiceDescriptorEx
    {
        public static bool IsComplete(this ServiceDescriptor sd)
        {
            return sd != null && (sd.ImplementationType != null || sd.ImplementationInstance != null || sd.ImplementationFactory != null);
        }
    }
}
