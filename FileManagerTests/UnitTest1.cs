using NUnit.Framework;
using System;
using FileManagerLibrary;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

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
            // FileManager.Files.TryGetValue(@"C:\Users\Gurrapettersson\source\github\FileHandler\FileManagerTests\test.txt", out string actual);

            // Assert.AreEqual(expected, actual);
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
        public void Test_ReadFile()
        {
            string filePath = Directory.GetCurrentDirectory() + "test.txt";
            FileManager.SaveFile("blalblbalbla", filePath);

            Assert.IsTrue(File.Exists(filePath[0..^4] + "_Modified.txt"));
        }

        #endregion

        #region BinarySearch Tests

        [Test]
        public void CountOccurencesOf_ReturnsZero()
        {
            List<string> list = new List<string>() { "a", "c", "d", "e", "f", "g", "e" };
            Assert.IsTrue(list.CountOccurencesOf("b") == 0);
        }

        [Test]
        public void CountOccurencesOf_SameStrings()
        {
            List<string> list = new List<string>() { "y", "y", "y", "y", "y", "y", "y", "y", "y", "y" };
            Assert.IsTrue(list.CountOccurencesOf("y") == 10);
        }

        [Test]
        public void CountOccurencesOf_CorrectCount()
        {
            List<string> list = new List<string>() { "a", "a", "c", "d", "e", "f", "g", "h", "i", "j" };
            Assert.IsTrue(list.CountOccurencesOf("a") == 2);
        }

        #endregion
    }
}