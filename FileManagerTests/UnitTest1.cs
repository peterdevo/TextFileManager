using NUnit.Framework;
using System;
using FileManagerLibrary;

namespace FileManagerTests
{
    public class Tests
    {
        [Test]
        public void TestFileRead()
        {
            FileManager fileManager = new FileManager();


            Assert.AreEqual(true, fileManager.ReadFile("C:\\Users\\Cazper\\source\\repos\\FileManager\\FileManagerTests\\test.txt"));
        }

        [Test]
        public void TestIncorrectFileTypeThrowsException()
        {
            FileManager fileManager = new FileManager();

            Assert.Throws<InvalidOperationException>(() => fileManager.ReadFile("example.exe"));
        }
    }
}