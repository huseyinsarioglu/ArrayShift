namespace ArrayShiftService
{
    public interface IShiftCalculatorService
    {
        void Calculate<T>(T[,] source, T[,] result, int shift);
    }

    public sealed class ShiftCalculatorService : IShiftCalculatorService
    {
        public void Calculate<T>(T[,] source, T[,] result, int shift)
        {
            (int row, int column) counts = (source.GetLength(0), source.GetLength(1));

            // convert it to clockwise if needed
            if (shift < 0)
                shift += source.Length;

            for (int currentRow = 0; currentRow < counts.row; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < counts.column; currentColumn++)
                {
                    var shiftedColumn = currentColumn + shift;
                    var newColumn = shiftedColumn % counts.column;
                    var newRow = ((shiftedColumn / counts.column) + currentRow) % counts.row;

                    result[newRow, newColumn] = source[currentRow, currentColumn];
                }
            }
        }
    }
}
