﻿/*using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        /// Эта функция уже реализована, она заработает, 
        /// как только вы выполните задачу в файле LeftBorderTask
        /// </remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            // тут стоит использовать написанный ранее класс LeftBorderTask
            return null;
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            // тут стоит использовать написанные ранее классы LeftBorderTask и RightBorderTask
            return -1;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            // ...
            //CollectionAssert.IsEmpty(actualTopWords);
        }

        // ...

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            // ...
            //Assert.AreEqual(expectedCount, actualCount);
        }

        // ...
    }
}
*/
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Autocomplete
{
    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void GetCountByPrefixTest()
        {
            var phrases = new List<string> { "a", "ab", "abc", "b" };
            var prefix = "a";
            var expectedResult = 3;
            var result = My_AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetCountByPrefixEmptyTest()
        {
            var phrases = new List<string> { "a", "ab", "abc", "b" };
            var prefix = "c";
            var expectedResult = 0;
            var result = My_AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void GetTopByPrefixTest()
        {
            var phrases = new List<string> { "a", "ab", "abc", "b" };
            var prefix = "a";
            var count = 2;
            var expectedResult = new[] { "a", "ab" };
            var result = My_AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void GetTopByPrefixTestLessThanCount()
        {
            var phrases = new List<string> { "a", "ab", "abc", "b" };
            var prefix = "a";
            var count = 4;
            var expectedResult = new[] { "a", "ab", "abc" };
            var result = My_AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void GetTopByPrefixTestEmpty()
        {
            var phrases = new List<string> { "a", "ab", "abc", "b" };
            var prefix = "c";
            var count = 2;
            var expectedResult = new string[0];
            var result = My_AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedResult, result);
        }
    }



    internal class My_AutocompleteTask
    {
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = My_LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            else
                return null;
        }


        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            int actualCount = Math.Min(GetCountByPrefix(phrases, prefix), count);
            int startIndex = My_LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            string[] result = new string[actualCount];
            Array.Copy(phrases.ToArray(), startIndex, result, 0, actualCount);
            return result;
        }


        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            int left = My_LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            int right = My_RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            return right - left - 1;
        }
    }
}