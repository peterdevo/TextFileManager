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
            string workingDirectory = Directory.GetCurrentDirectory();

            string projectDirectory = Directory.GetParent(Directory.GetParent(workingDirectory).ToString()).Parent.FullName;

            FileManager.ReadFile(projectDirectory + "\\TestFiles\\test.txt");
            List<string> expected = new List<string>() 
            {
                "abc",
                "def",
                "fds",
                "afd",
                "kter",
                "fdes"
            };

            FileManager.Files.TryGetValue(projectDirectory + "\\TestFiles\\test.txt", out List<string> actual);

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
        public void Test_SaveFile()
        {
            // Arrange
            string workingDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(workingDirectory).ToString()).Parent.FullName;
            string filePath = projectDirectory + "\\TestFiles\\a.txt";
            // Act
            FileManager.SaveFile(filePath, "asdsadasd");

            // Assert
            Assert.IsTrue(File.Exists(filePath[0..^4] + "_Modified.txt"));
        }

        #endregion

        #region WordOccurence Method Tests

        [Test]
        public void Test_WordOccurrence()
        {
            // Arrange
            string text = "Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. " +
                "Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean. " +
                "A small river named Duden flows by their place and supplies it with the necessary regelialia. ";
            string workingDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(workingDirectory).ToString()).Parent.FullName;
            string filePath = projectDirectory + "\\TestFiles\\WordOccurrences.txt";
            string actual = "", expected = "3";
            
            // Act
            File.WriteAllText(filePath, text);
            FileManager.ReadFile(filePath);
            actual = FileManager.WordOccurrences("far")[2];

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_WordOccurrence_WithMoreThanOneWord()
        {
            // Arrange
            string text = "Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. " +
                "Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean. " +
                "A small river named Duden flows by their place and supplies it with the necessary regelialia. ";
            string workingDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(workingDirectory).ToString()).Parent.FullName;
            string filePath = projectDirectory + "\\TestFiles\\WordOccurrences.txt";
            string[] result = null;

            // Act
            FileManager.ReadFile(filePath);
            File.WriteAllText(filePath, text);
            result = FileManager.WordOccurrences("far far");

            // Assert
            Assert.IsNull(result);
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

        #region Quick Sort test
        [Test]
        public void CheckIfSortInOrder()
        {
            List<string> testList = new List<string> {"b","c","a","g","f"};
            QuickSort<string>.SortQuick(ref testList,0,testList.Count-1);
            List<string> expectedList = new List<string> { "a", "b", "c","f","g" };
            CollectionAssert.AreEqual(expectedList,testList);
        }

        [Test]
        public void CheckIfItSortsTheSortedList()
        {
            List<string> testList = new List<string> { "a", "b", "c", "d","e" };
            QuickSort<string>.SortQuick(ref testList, 0, testList.Count - 1);
            List<string> expectedList = new List<string> { "a","b","c","d","e" };
            CollectionAssert.AreEqual(expectedList, testList);
        }
        [Test]
        public void checkIfDuplicatedElementSortCorrectly()
        {
            List<string> testList = new List<string> { "b", "c", "c", "d", "d","b" };
            QuickSort<string>.SortQuick(ref testList, 0, testList.Count - 1);
            List<string> expectedList = new List<string> { "b", "b", "c", "c", "d","d" };
            CollectionAssert.AreEqual(expectedList, testList);
        }

        [Test]
        public void CheckIfItIsStable()
        {
            List<string> testList = new List<string> { "b", "a", "c", "c'", "d", };
            QuickSort<string>.SortQuick(ref testList, 0, testList.Count - 1);
            List<string> expectedList = new List<string> { "a","b","c","c'","d" };
            CollectionAssert.AreEqual(expectedList, testList);
        }

        #endregion

    }
}