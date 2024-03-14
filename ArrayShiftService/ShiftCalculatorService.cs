namespace ArrayShiftService
{
    public interface IShiftCalculatorService
    {
        T[,] Calculate<T>(T[,] source, int shift);
    }

    public sealed class ShiftCalculatorService : IShiftCalculatorService
    {
        private (int row, int column) _counts;

        public T[,] Calculate<T>(T[,] source, int shift)
        {
            _counts = (source.GetLength(0), source.GetLength(1));

            // convert it to clockwise if needed
            if (shift < 0)
                shift += source.Length;

            return GetResult(source, shift);
        }

        private T[,] GetResult<T>(T[,] source, int shift)
        {
            var result = new T[_counts.row, _counts.column];

            for (int currentRow = 0; currentRow < _counts.row; currentRow++)
            {
                FillRow(currentRow, source, result, shift);
            }

            return result;
        }

        private void FillRow<T>(int currentRow, T[,] source, T[,] result, int shift)
        {
            for (int currentColumn = 0; currentColumn < _counts.column; currentColumn++)
            {
                var location = CalculateNewLocation(shift, (currentRow, currentColumn));
                result[location.row, location.column] = source[currentRow, currentColumn];
            }
        }

        private (int row, int column) CalculateNewLocation(int shift, (int row, int column) current)
        {
            var shiftedColumn = current.column + shift;
            var calculatedColumn = shiftedColumn % _counts.column;
            var calculatedRow = ((shiftedColumn / _counts.column) + current.row) % _counts.row;

            return (calculatedRow, calculatedColumn);
        }
    }
}
