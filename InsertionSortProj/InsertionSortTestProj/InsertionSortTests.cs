using NUnit.Framework;

namespace InsertionSortProj
{
    [TestFixture]
    public class InsertionSortTests
    {
        [Test]
        public void RandomArraySort()
        {
            CompareInsertionSortWithSystemSort(CreateIntArr());
        }

        [Test]
        public void PositiveNumbersSort()
        {
            CompareInsertionSortWithSystemSort(new int[] { 6, 2, 9, 20, 130 });
        }
        
        [Test]
        public void NegativeAndPositiveNumbersSort()
        {
            CompareInsertionSortWithSystemSort(new int[] { -2, 5, 0, -16, 100 });
        }

        [Test]
        public void JustNegativeNumbersSort()
        {
            CompareInsertionSortWithSystemSort(new int[] { -2, -5, -3, -16, -100 });
        }

        [Test]
        public void EmptyListSort()
        {
            CompareInsertionSortWithSystemSort(Array.Empty<int>());
        }

        private void CompareInsertionSortWithSystemSort(int[] arr)
        {
            int[] expectedArr = arr.ToArray();
            Array.Sort(expectedArr);
            InsertionSort.Sort(arr);
            CollectionAssert.AreEqual(expectedArr, arr);
        }

        private int[] CreateIntArr()
        {
            Random rnd = new Random();
            int[] numbers = Enumerable.Range(1, rnd.Next(1,400)).OrderBy(r => rnd.Next()).ToArray();
            return numbers;
        }
    }
}