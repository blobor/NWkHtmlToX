using System;

namespace NWkHtmlToX.Infrastructure.PathResolvers {
    public class PdfDllRegistryPathResolver : RegistryBasePathResolver {

        private const string WKHTMLTOPDF_REGISTRY_PATH = @"SOFTWARE\wkhtmltopdf";

        public override string ResolvePath() {
            using (var localMachineRegistry = GetLocalMachineRegistryKey())
            using (var wkhtmltopdfKey = localMachineRegistry.OpenSubKey(WKHTMLTOPDF_REGISTRY_PATH)) {
                return wkhtmltopdfKey?.GetValue(WKHTMLTOX_DLL_PATH_REGISTRY_KEY, String.Empty).ToString() ?? String.Empty;
            }
        }
    }
}