using System;
using System.Threading;
using Xunit;

namespace DirFinderLib.Tests
{
    public class MyThreadPoolTests
    {
        #region snippet_SuppressThread_Passes_InputIsCorrect

        [Fact]
        public void SuppressThread_Passes_InputIsCorrect()
        {
            // Arrange
            var number = 1;

            // Act
            MyThreadPool.SuppressThread(() => number++);
            Thread.Sleep(100);

            // Assert
            Assert.Equal(2, number);
        }

        #endregion

        #region snippet_SuppressThread_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void SuppressThread_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange&&Act
            static void Result()
            {
                MyThreadPool.SuppressThread(null);
            }


            // Assert
            Assert.Throws<ArgumentNullException>(Result);
        }

        #endregion

        #region snippet_ReleaseThread_Passes_InputIsCorrect

        [Fact]
        public void ReleaseThread_Passes_InputIsCorrect()
        {
            // Arrange
            var sleepTime = 100;
            var threadCount = MyThreadPool.Count;
            // Act
            MyThreadPool.SuppressThread(() => Thread.Sleep(sleepTime));

            // Assert
            Assert.True(threadCount > MyThreadPool.Count);
        }

        #endregion
    }
}