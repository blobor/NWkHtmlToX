using System;
using System.IO;
using NWkHtmlToX.Common.Native;
using NWkHtmlToX.Common.Native.Win32;
using NWkHtmlToX.Common.SafeHandles;
using Xunit;

namespace NWkHtmlToX.Common.Tests.Native {
    public class WindowsLibraryLoaderTests {

        private readonly ILibraryLoader _libraryLoader;

        private readonly string _windowsValidDllPath;

        public WindowsLibraryLoaderTests() {
            _libraryLoader = new WindowsLibraryLoader();

            _windowsValidDllPath = Path.Combine(Environment.SystemDirectory, "user32.dll");
        }

        #region LoadLibrary

        [Fact]
        public void LoadLibrary_ShouldReturnValidLibraryHandle() {
            // Act
            var handle = _libraryLoader.LoadLibrary(_windowsValidDllPath);

            // Assert
            Assert.False(handle.IsInvalid);
            Assert.False(handle.IsClosed);
        }

        [Fact]
        public void LoadLibrary_ShouldThrowExceptionIfDllPathIsNull() {
            // Arrange
            Action fucntion = () => _libraryLoader.LoadLibrary(null);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(fucntion);
        }

        [Fact]
        public void LoadLibrary_ShouldThrowExceptionIfDllDoesntExist() {
            // Arrange
            var invalidPath = Path.Combine(Environment.SystemDirectory, DateTime.Now.Ticks.ToString());
            Action fucntion = () => _libraryLoader.LoadLibrary(invalidPath);

            // Act, Assert
            Assert.Throws<FileNotFoundException>(fucntion);
        }

        #endregion // LoadLibrary

        #region FreeLibrary
        [Fact]
        public void FreeLibrary_ShouldReturnFalseIfHadleIsInvalid() {
            // Arrange
            var handle = new SafeWindowsLibraryHandle();
            handle.SetHandleAsInvalid();

            // Act
            var result = _libraryLoader.FreeLibrary(handle);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FreeLibrary_ShouldReturnFalseIfHadleIsClosed() {
            // Arrange
            var handle = new SafeWindowsLibraryHandle();
            handle.Close();

            // Act
            var result = _libraryLoader.FreeLibrary(handle);

            // Assert
            Assert.False(result);
        }

        #endregion // FreeLibrary
    }
}