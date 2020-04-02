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

        [Test]
        public void CheckIfSortInOrder()
        {
            List<string> testList = new List<string> {"b","c","a","g","f"};
            QuickSort<string>.SortQuick(testList,0,testList.Count-1);
            List<string> expectedList = new List<string> { "a", "b", "c","f","g" };
            CollectionAssert.AreEqual(expectedList,testList);
        }

        [Test]
        public void CheckIfItSortsTheSortedList()
        {
            List<string> testList = new List<string> { "a", "b", "c", "d","e" };
            QuickSort<string>.SortQuick(testList, 0, testList.Count - 1);
            List<string> expectedList = new List<string> { "a","b","c","d","e" };
            CollectionAssert.AreEqual(expectedList, testList);
        }
        [Test]
        public void checkIfDuplicatedElementSortCorrectly()
        {
            List<string> testList = new List<string> { "b", "c", "c", "d", "d","b" };
            QuickSort<string>.SortQuick(testList, 0, testList.Count - 1);
            List<string> expectedList = new List<string> { "b", "b", "c", "c", "d","d" };
            CollectionAssert.AreEqual(expectedList, testList);
        }

        [Test]
        public void CheckIfItIsStable()
        {
            List<string> testList = new List<string> { "b", "a", "c", "c'", "d", };
            QuickSort<string>.SortQuick(testList, 0, testList.Count - 1);
            List<string> expectedList = new List<string> { "a","b","c","c'","d" };
            CollectionAssert.AreEqual(expectedList, testList);
        }

    }
}