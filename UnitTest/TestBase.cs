namespace UnitTest
{
    [TestFixture]
    public class TestBase
    {
        protected static void AssertResult(char[,] result, char[,] expected)
        {
            int rowCount = result.GetLength(0);
            int columnCount = result.GetLength(1);

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    Assert.That(result[row, column], Is.EqualTo(expected[row, column]), $"row: {row}, column: {column}");
                }
            }
        }
    }
}
