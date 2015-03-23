using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NWkHtmlToX.Common.Threading;
using Xunit;

namespace NWkHtmlToX.Common.Tests.Threading {
    public class STATaskSchedulerTests {

        public static IEnumerable<object[]> STATaskSchedulers {
            get {
                yield return new object[] { new STAThreadPerTaskScheduler() };
                yield return new object[] { LimitedConcurrencyLevelSTATaskScheduler.SingleSTAThreadTaskScheduler };
            }
        }

        [Theory, MemberData(nameof(STATaskSchedulers))]
        public void TaskScheduler_ShouldScheduleTaskWithSTAThreads(TaskScheduler testTaskScheduler) {
            // Act, Assert
            Task.Factory.StartNew(() => Assert.Equal(ApartmentState.STA, Thread.CurrentThread.GetApartmentState()), CancellationToken.None, TaskCreationOptions.DenyChildAttach, testTaskScheduler);
        }

        [Theory, MemberData(nameof(STATaskSchedulers))]
        public async Task TaskScheduler_ShouldScheduleTaskWithSTAThreadsAsync(TaskScheduler testTaskScheduler) {
            // Act, Assert
            await Task.Factory.StartNew(async () => {
                Assert.Equal(ApartmentState.STA, Thread.CurrentThread.GetApartmentState());
                await Task.Yield();
                Assert.Equal(ApartmentState.STA, Thread.CurrentThread.GetApartmentState());
            }, CancellationToken.None, TaskCreationOptions.DenyChildAttach, testTaskScheduler);
        }

        [Theory, MemberData(nameof(STATaskSchedulers))]
        public async Task TaskScheduler_ThreadShouldHaveSameAparmentStateAfterAsyncOperation(TaskScheduler testTaskScheduler) {
            // Arrange
            var state = Thread.CurrentThread.GetApartmentState();

            // Acts
            await Task.Factory.StartNew(async () => await Task.Yield(), CancellationToken.None, TaskCreationOptions.DenyChildAttach, testTaskScheduler);

            // Assert
            Assert.Equal(state, Thread.CurrentThread.GetApartmentState());
        }
    }
}