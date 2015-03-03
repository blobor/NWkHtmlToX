using System;
using System.IO;
using NWkHtmlToX.Common.Native;
using NWkHtmlToX.Common.Native.Win32;
using Xunit;

namespace NWkHtmlToX.Common.Tests.Native {
    public class WindowsLibraryLoaderTests {

        private readonly ILibraryLoader _libraryLoader;

        private readonly string _windowsValidDllPath;

        public WindowsLibraryLoaderTests() {
            _libraryLoader = new WindowsLibraryLoader();

            _windowsValidDllPath = Path.Combine(Environment.SystemDirectory, "user32.dll");
        }

        [Fact]
        public void LibraryLoaderShouldReturnValidLibraryHandle() {
            // Act
            var handle = _libraryLoader.LoadLibrary(_windowsValidDllPath);

            // Assert
            Assert.False(handle.IsInvalid);
            Assert.False(handle.IsClosed);
        }

        [Fact]
        public void LibraryLoaderShouldThrowExceptionIfDllPathIsNull() {
            // Arrange
            Action fucntion = () => _libraryLoader.LoadLibrary(null);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(fucntion);
        }

        [Fact]
        public void LibraryLoaderShouldThrowExceptionIfDllDoesntExist() {
            // Arrange
            var invalidPath = Path.Combine(Environment.SystemDirectory, DateTime.Now.Ticks.ToString());
            Action fucntion = () => _libraryLoader.LoadLibrary(invalidPath);

            // Act, Assert
            Assert.Throws<FileNotFoundException>(fucntion);
        }
    }
}