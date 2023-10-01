using Microsoft.Extensions.DependencyInjection;

namespace Clio.Demo.Core.Extension
{
    public static class ServiceDescriptorEx
    {
        public static bool IsComplete(this ServiceDescriptor sd)
        {
            return sd is not null && (sd.ImplementationType is not null || sd.ImplementationInstance is not null || sd.ImplementationFactory is not null);
        }
    }
}
