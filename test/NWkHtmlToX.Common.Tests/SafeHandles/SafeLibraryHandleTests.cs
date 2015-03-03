using NWkHtmlToX.Common.SafeHandles;
using Xunit;

namespace NWkHtmlToX.Common.Tests.SafeHandles {
    public class SafeLibraryHandleTests {

        [Fact]
        public void SafeLibraryHandleShouldBeInvalidByDefault() {
            // Arrange, Act
            SafeLibraryHandle[] handles = { new SafeWindowsLibraryHandle() };

            // Assert
            foreach (var handle in handles) {
                Assert.True(handle.IsInvalid);
            }
        }
    }
}