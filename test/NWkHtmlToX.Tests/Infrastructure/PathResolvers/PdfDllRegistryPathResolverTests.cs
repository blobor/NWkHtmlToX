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
            // Arrange

            // Act
            var result = _registryPathResolver.ResolvePath();

            // Assert
            Assert.NotNull(result);
        }
    }
}