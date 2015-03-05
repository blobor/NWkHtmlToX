using System;
using System.Collections.Generic;
using System.Threading;
using NWkHtmlToX.Common.Threading;
using Xunit;

namespace NWkHtmlToX.Common.Tests.Threading {
    public class STAThreadFactoryTests {

        private readonly IThreadFactory _threadFactory;

        public STAThreadFactoryTests() {
            _threadFactory = new STAThreadFactory();
        }

        [Fact]
        public void Create_ShouldThrowExceptionIfDelegateIsNull() {
            // Arrange
            Action functionWithoutArguments = () => _threadFactory.Create(null as Action);
            Action functionWithArgument = () => _threadFactory.Create(null as Action<object>);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(functionWithoutArguments);
            Assert.Throws<ArgumentNullException>(functionWithArgument);
        }

        [Fact]
        public void Create_ShouldReturnThreadWithSTAApartmentState() {
            // Arrange
            var threads = new List<Thread>(2);
            Action functionWithoutArguments = () => Assert.Equal(ApartmentState.STA, Thread.CurrentThread.GetApartmentState());
            Action<object> functionWithArgument = obj => Assert.Equal(ApartmentState.STA, Thread.CurrentThread.GetApartmentState());

            // Act
            threads.Add(_threadFactory.Create(functionWithArgument));
            threads.Add(_threadFactory.Create(functionWithoutArguments));

            // Assert
            foreach (var thread in threads) {
                thread.Start();
                thread.Join();
            }
        }
    }
}