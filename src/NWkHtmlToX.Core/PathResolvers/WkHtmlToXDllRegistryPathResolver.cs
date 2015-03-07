using System;
using Microsoft.Win32;

namespace NWkHtmlToX.Core.PathResolvers {
    public sealed class WkHtmlToXDllRegistryPathResolver : IPathResolver {

        public const string WKHTMLTOX_DLL_PATH_REGISTRY_KEY = "DllPath";
        public const string WKHTMLTOPDF_REGISTRY_PATH = @"SOFTWARE\wkhtmltopdf";

        private RegistryKey GetLocalMachineRegistryKey() {
            var registryView = Environment.Is64BitOperatingSystem
                                                          ? RegistryView.Registry64
                                                          : RegistryView.Registry32;

            return RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView);
        }

        public string ResolvePath() {
            using (var localMachineRegistry = GetLocalMachineRegistryKey())
            using (var wkhtmltopdfKey = localMachineRegistry.OpenSubKey(WKHTMLTOPDF_REGISTRY_PATH)) {
                return wkhtmltopdfKey?.GetValue(WKHTMLTOX_DLL_PATH_REGISTRY_KEY)?.ToString();
            }
        }
    }
}