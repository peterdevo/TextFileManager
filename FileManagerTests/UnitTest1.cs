using NUnit.Framework;
using System;
using FileManagerLibrary;
using System.IO;
using System.Reflection;

namespace FileManagerTests
{
    public class Tests
    {
        #region ReadFile Method Tests

        [Test]
        public void Test_FileRead()
        {
            FileManager.ReadFile(@"C:\Users\Gurrapettersson\source\github\FileHandler\FileManagerTests\test.txt");
            string expected = "Hejsan";
            FileManager.Files.TryGetValue(@"C:\Users\Gurrapettersson\source\github\FileHandler\FileManagerTests\test.txt", out string actual);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_ThatIncorrectFileType_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => FileManager.ReadFile("example.exe"));
        }

        [Test]
        public void Test_ThatNonExistentFile_ThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() => FileManager.ReadFile("FinnsGaranteratInte.txt"));
        }

        #endregion

        #region SaveFile Method Tests

        [Test]
        public void Test_SavingFileWithIncorrectFilePath()
        {

        }

        #endregion
    }
}