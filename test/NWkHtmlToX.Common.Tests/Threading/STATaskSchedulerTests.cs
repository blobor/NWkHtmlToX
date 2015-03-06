using System.Threading;
using System.Threading.Tasks;
using NWkHtmlToX.Common.Threading;
using Xunit;

namespace NWkHtmlToX.Common.Tests.Threading {
    public class STATaskSchedulerTests {
        private readonly TaskFactory _taskFactory;

        public STATaskSchedulerTests() {
            TaskScheduler taskScheduler = new STATaskScheduler(new STAThreadFactory());
            _taskFactory = new TaskFactory(taskScheduler);
        }

        [Fact]
        public void TaskScheduler_ShouldScheduleTaskWithSTAThreads() {
            // Act, Assert
            _taskFactory.StartNew(() => Assert.Equal(ApartmentState.STA, Thread.CurrentThread.GetApartmentState()));
        }

        [Fact]
        public async Task TaskScheduler_ShouldScheduleTaskWithSTAThreadsAsync() {
            // Act, Assert
            await _taskFactory.StartNew(async () => {
                Assert.Equal(ApartmentState.STA, Thread.CurrentThread.GetApartmentState());
                await Task.Yield();
                Assert.Equal(ApartmentState.STA, Thread.CurrentThread.GetApartmentState());
            });
        }

        [Fact]
        public async Task TaskScheduler_ThreadShouldHaveSameAparmentStateAfterAsyncOperation() {
            // Arrange
            var state = Thread.CurrentThread.GetApartmentState();

            // Acts
            await _taskFactory.StartNew(async () => await Task.Yield());

            // Assert
            Assert.Equal(state, Thread.CurrentThread.GetApartmentState());
        }
    }
}