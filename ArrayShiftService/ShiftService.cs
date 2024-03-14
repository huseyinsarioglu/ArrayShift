namespace ArrayShiftService
{
    public class ShiftService
    {
        private readonly IShiftCalculatorService _calculatorService;

        public ShiftService(IShiftCalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        public T[,] Shift<T>(T[,] arr, int shift)
        {
            (int row, int column) counts = (arr?.GetLength(0) ?? 0, arr?.GetLength(1) ?? 0);

            var result = new T[counts.row, counts.column];

            if (arr == null || arr.Length == 0)
                return result;

            // eliminate circular shift
            shift %= arr.Length;

            if (shift == 0)
            {
                Array.Copy(arr, result, arr.Length);
            }
            else
            {
                _calculatorService.Calculate(arr, result, shift);
            }

            return result;
        }
    }
}
