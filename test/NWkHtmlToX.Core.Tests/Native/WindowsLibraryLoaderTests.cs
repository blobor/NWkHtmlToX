using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using NWkHtmlToX.Core.Native;
using NWkHtmlToX.Core.Native.Win32;
using Xunit;

namespace NWkHtmlToX.Core.Tests.Native {
    public class WindowsLibraryLoaderTests {

        private readonly ILibraryLoader _windowsLibraryLoader;

        public WindowsLibraryLoaderTests() {
            _windowsLibraryLoader = new WindowsLibraryLoader();
        }

        [Fact]
        public void LoadLibraryMethodShoudThrowArgumentNullExceptionOnNullArgument() {
            // Arrange
            string testArgument = null;
            Action testCode = () => _windowsLibraryLoader.LoadLibrary(testArgument);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(testCode);
        }

        [Fact]
        public void LoadLibraryMethodShoudThrowFileNotFoundExceptionIfLibraryWasNotFound() {
            // Arrange
            var testArgument = String.Format(@"C:\NotExistingFile_{0}.dll", DateTime.Now.Ticks.ToString(NumberFormatInfo.InvariantInfo));
            Action testCode = () => _windowsLibraryLoader.LoadLibrary(testArgument);

            // Act, Assert
            Assert.Throws<FileNotFoundException>(testCode);
        }

        [Fact]
        public void LoadLibraryMethodShouldLoadLibraryByPathAndReturnValidHandle() {
            // Arrange
            const string dllPath = @"C:\Windows\System32\user32.dll";

            // Act
            var handle = _windowsLibraryLoader.LoadLibrary(dllPath);

            // Assert
            Assert.NotEqual(IntPtr.Zero, handle);
        }

        [Fact]
        public void LoadLibraryMethodShouldThrowExcpetionIfHandleIsNotValid() {
            // Arrange
            // wkhtmltox should run only in single appartment mode, in another case it will crash
            const string dllPath = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltox.dll";
            Action testFunction = () => _windowsLibraryLoader.LoadLibrary(dllPath);

            // Act, Assert
            Assert.Throws<Win32Exception>(testFunction);
        }

        [Fact]
        public void GetProcAddressMethodShoudThrowArgumentExceptionOnInvalidHandle() {
            // Arrange
            var handle = IntPtr.Zero;
            var procedureName = String.Empty;
            Action testCode = () => _windowsLibraryLoader.GetProcAddress(handle, procedureName);

            // Act, Assert
            Assert.Throws<ArgumentException>(nameof(handle), testCode);
        }

        [Fact]
        public void GetProcAddressMethodShoudThrowArgumentNullExceptionOnNullProcedureNameArgument() {
            // Arrange
            var handle = new IntPtr(int.MinValue);
            string procedureName = null;
            Action testCode = () => _windowsLibraryLoader.GetProcAddress(handle, procedureName);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(testCode);
        }
    }
}