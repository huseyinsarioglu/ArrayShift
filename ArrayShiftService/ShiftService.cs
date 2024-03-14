namespace ArrayShiftService
{
    public class ShiftService
    {
        private readonly IShiftCalculatorService _calculatorService;

        public ShiftService(IShiftCalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        public T[,] Shift<T>(T[,]? arr, int shift)
        {
            if (arr == null || arr.Length == 0)
            {
                return new T[0, 0];
            }

            // eliminate circular shift before sending it to the calculator
            return GetResult(arr, shift % arr.Length);
        }

        private T[,] GetResult<T>(T[,] arr, int shift)
        {
            if (shift == 0)
            {
                return (T[,])arr.Clone();
            }

            return _calculatorService.Calculate(arr, shift);
        }
    }
}
