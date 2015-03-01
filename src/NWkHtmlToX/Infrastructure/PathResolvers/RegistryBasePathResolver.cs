using System;
using Microsoft.Win32;

namespace NWkHtmlToX.Infrastructure.PathResolvers {
    public abstract class RegistryBasePathResolver : IPathResolver {

        protected const string WKHTMLTOX_DLL_PATH_REGISTRY_KEY = "DllPath";

        protected RegistryKey GetLocalMachineRegistryKey() {
            var registryView = Environment.Is64BitOperatingSystem
                                                          ? RegistryView.Registry64
                                                          : RegistryView.Registry32;

            return RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView);
        }

        public abstract string ResolvePath();
    }
}