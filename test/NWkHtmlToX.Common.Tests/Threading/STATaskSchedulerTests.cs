using System.Threading;
using System.Threading.Tasks;
using NWkHtmlToX.Common.Threading;
using Xunit;

namespace NWkHtmlToX.Common.Tests.Threading {
    public class STATaskSchedulerTests {

        private readonly TaskScheduler _taskScheduler;
        
        public STATaskSchedulerTests() {
            _taskScheduler = new STATaskScheduler(new STAThreadFactory());
        }

        [Fact]
        public void TaskScheduler_ShouldScheduleTaskWithSTAThreads() {
            // Arrange
            var factory = new TaskFactory(_taskScheduler);

            // Act, Assert
            factory.StartNew(() => Assert.Equal(ApartmentState.STA, Thread.CurrentThread.GetApartmentState()));
        }

        [Fact]
        public async Task TaskScheduler_ShouldScheduleTaskWithSTAThreadsAsync() {
            // Arrange
            var factory = new TaskFactory(_taskScheduler);

            // Act, Assert
            await factory.StartNew(() => Assert.Equal(ApartmentState.STA, Thread.CurrentThread.GetApartmentState()));
        }
    }
}