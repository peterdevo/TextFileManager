using NUnit.Framework;
using System;
using FileManagerLibrary;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace FileManagerTests
{
    public class Tests
    {
        #region Directory Variables

        private static string workingDirectory = Directory.GetCurrentDirectory();
        private static string projectDirectory = Directory.GetParent(Directory.GetParent(workingDirectory).ToString()).Parent.FullName;

        #endregion

        #region ReadFile Tests

        [Test]
        public void Test_FileRead()
        {
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

        #region SortCollection Tests

        [Test]
        public void Test_SortCollection()
        {
            // Arrange 
            var expected = new List<string>();
            var actual = new List<string>();
            string filePath = projectDirectory + "\\TestFiles\\test.txt";

            // Act
            FileManager.ReadFile(filePath);
            expected = FileManager.Files[filePath].OrderBy(x => x[0]).ToList();

            FileManager.SortCollection(filePath);
            FileManager.SaveFile(filePath);
            actual = File.ReadAllText(filePath[0..^4] + "_Modified.txt").TrimEnd().Split(" ").ToList();

            // Assert
            foreach (var item in expected)
            {
                Assert.IsTrue(actual.Contains(item));
            }

        }

        [Test]
        public void Test_ThatSortCollection_ThrowsException_WhenFileIsEmpty()
        {
            // Arrange
            string filePath = projectDirectory + "\\TestFiles\\empty.txt";

            // Act
            FileManager.ReadFile(filePath);

            // Assert
            Assert.Throws<InvalidOperationException>(() => FileManager.SortCollection(filePath));
        }

        #endregion

        #region SaveFile Tests

        [Test]
        public void Test_SaveFile()
        {
            // Arrange
            string filePath = projectDirectory + "\\TestFiles\\a.txt";
            
            // Act
            FileManager.SaveFile(filePath);

            // Assert
            Assert.IsTrue(File.Exists(filePath[0..^4] + "_Modified.txt"));
        }



        #endregion

        #region WordOccurence Tests

        [Test]
        public void Test_WordOccurrence()
        {
            // Arrange
            string text = "Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. " +
                "Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean. " +
                "A small river named Duden flows by their place and supplies it with the necessary regelialia. ";
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

        #region Quick Sort Tests
        [Test]
        public void CheckIfSortAscesdingInOrder()
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
            List<string> expectedList = new List<string> { "a", "b", "c", "c'", "d" };
            CollectionAssert.AreEqual(expectedList, testList);
        }
        [Test]
        public void CheckCharacterÄÖÅ()
        {
            List<string> testList = new List<string> { "Jag", "gillar", "äta","mat","å","ögon"};
            QuickSort<string>.SortQuick(ref testList, 0, testList.Count - 1);
            List<string> expectedList = new List<string> { "gillar", "Jag", "mat","å","äta","ögon" };
            CollectionAssert.AreEqual(expectedList, testList);
        }

        [Test]
        public void CheckSpecialCharacter()
        {
            List<string> testList = new List<string> { "!", "0", "1", "2", "3" ,"d","b","a", "()"};
            QuickSort<string>.SortQuick(ref testList, 0, testList.Count - 1);
            List<string> expectedList = new List<string> { "!", "()", "0", "1", "2", "3","a","b","d"};
            CollectionAssert.AreEqual(expectedList, testList);
        }

        [Test]
        public void ThrowNullExceptionIfListIsNull()
        {
            List<string> testList = null;

            Assert.Throws<NullReferenceException>(() => QuickSort<string>.SortQuick(ref testList, 0, testList.Count - 1));
        }
        [Test]
        public void ThrowIndexOutOfRangeExceptionIfLeftIslessThan0()
        {
            List<string> testList = new List<string> {"b","a"};

            Assert.Throws<IndexOutOfRangeException>(() => QuickSort<string>.SortQuick(ref testList, -1, testList.Count));
        }

        [Test]
        public void ThrowIndexOutOfRangeExceptionIfRightisIsGreaterThanOrEqualToCount()
        {
            List<string> testList = new List<string> { "b", "a" };

            Assert.Throws<IndexOutOfRangeException>(() => QuickSort<string>.SortQuick(ref testList, 0, testList.Count));
        }

       

        #endregion

        #region Read, Sort and Save File Integration Test

        [Test]
        public void ReadFile_SortTheFile_SaveTheFile()
        {
            // Arrange
            string filePath = projectDirectory + "\\TestFiles\\test.txt";
            var expected = new List<string>();
            var actual = new List<string>();

            // Act
            FileManager.ReadFile(filePath);
            FileManager.SortCollection(filePath);
            FileManager.SaveFile(filePath);
            expected = FileManager.Files[filePath].OrderBy(x => x[0]).ToList();
            actual = File.ReadAllText(filePath[0..^4] + "_Modified.txt").TrimEnd().Split(" ").ToList();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

    }
}