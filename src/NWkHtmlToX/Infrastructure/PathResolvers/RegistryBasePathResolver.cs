using System;
using Microsoft.Win32;

namespace NWkHtmlToX.Infrastructure.PathResolvers {
    public abstract class RegistryBasePathResolver : IPathResolver {

        protected const string WKHTMLTOX_DLL_PATH_REGISTRY_KEY = "DllPath";

        protected RegistryKey GetLocalMachineRegistryKey() {
#if ASPNETCORE50
            // Currently in .NET Core there are no possibility to check is system is 64 bit
            return Registry.LocalMachine;
#else
            var registryView = Environment.Is64BitOperatingSystem
                                                          ? RegistryView.Registry64
                                                          : RegistryView.Registry32;

            return RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView);
#endif
        }

        public abstract string ResolvePath();
    }
}