using NWkHtmlToX.Infrastructure;
using NWkHtmlToX.Infrastructure.PathResolvers;

using Xunit;

namespace NWkHtmlToX.Tests.Infrastructure.PathResolvers {
    public class PdfDllRegistryPathResolverTests {

        private readonly IPathResolver _registryPathResolver;

        public PdfDllRegistryPathResolverTests() {
            _registryPathResolver = new PdfDllRegistryPathResolver();
        }

        [Fact]
        public void DefaultResultShouldNotBeNull() {
            var result = _registryPathResolver.ResolvePath();

            Assert.NotNull(result);
        }
    }
}