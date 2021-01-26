using System;
using Xunit;

namespace DirFinderLib.Tests
{
    public class MultiDirFinderTests
    {
        #region snippet_CountDirs_Passes_InputIsCorrect

        [Fact]
        public void CountDirs_Passes_InputIsCorrect()
        {
            // Arrange
            MyThreadPool ThreadPool;

            // Assert&&Act
            MultiDirFinder.CountDirs(@"C:\Users\Argiziont\Downloads\Westworld");
        }

        #endregion

        #region snippet_CountDirs_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void CountDirs_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange&&Act
            static void Result()
            {
                MultiDirFinder.CountDirs(null);
            }


            // Assert
            Assert.Throws<ArgumentNullException>(Result);
        }

        #endregion

        #region snippet_CountDirs_ThrowsArgumentException_InputIsIncorrect

        [Fact]
        public void CountDirs_ThrowsArgumentException_InputIsIncorrect()
        {
            // Arrange&&Act
            static void Result()
            {
                MultiDirFinder.CountDirs(@"///");
            }


            // Assert
            Assert.Throws<ArgumentException>(Result);
        }

        #endregion
    }
}