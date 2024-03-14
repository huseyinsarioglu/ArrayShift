using ArrayShiftService;
using Moq;

namespace UnitTest
{
    public class CalculatorServiceTests : TestBase
    {
        private ShiftService _shiftService;
        private Mock<IShiftCalculatorService> _mockCalculatorService;

        [SetUp]
        public void Setup()
        {
            var calculatorService = new ShiftCalculatorService();
            _mockCalculatorService = new Mock<IShiftCalculatorService>();
            _mockCalculatorService
                .Setup(x => x.Calculate(It.IsAny<char[,]>(), It.IsAny<char[,]>(), It.IsAny<int>()))
                .Callback((char[,] source, char[,] result, int shift) =>
                {
                    calculatorService.Calculate(source, result, shift);
                });

            _shiftService = new ShiftService(_mockCalculatorService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockCalculatorService.Verify(x => x.Calculate(It.IsAny<char[,]>(), It.IsAny<char[,]>(), It.IsAny<int>()), Times.Once);
            _mockCalculatorService.VerifyNoOtherCalls();
        }

        [TestCase(1)]
        [TestCase(57)]
        public void Shift_One_Test(int shift)
        {
            var arr = new char[,]
            {
                { '1', '0', '1', '0', '1', '0', '0', '0' },
                { '1', '1', '0', '0', '1', '0', '1', '0' },
                { '1', '1', '0', '0', '0', '1', '1', '0' },
                { '1', '1', '0', '1', '0', '0', '0', '0' },
                { '1', '0', '0', '0', '0', '1', '0', '0' },
                { '1', '1', '1', '0', '1', '0', '1', '0' },
                { '1', '1', '0', '0', '1', '0', '0', '0' },
            };

            var result = _shiftService.Shift(arr, shift);

            var expected = new char[,]
            {
                { '0', '1', '0', '1', '0', '1', '0', '0' },
                { '0', '1', '1', '0', '0', '1', '0', '1' },
                { '0', '1', '1', '0', '0', '0', '1', '1' },
                { '0', '1', '1', '0', '1', '0', '0', '0' },
                { '0', '1', '0', '0', '0', '0', '1', '0' },
                { '0', '1', '1', '1', '0', '1', '0', '1' },
                { '0', '1', '1', '0', '0', '1', '0', '0' },
            };

            AssertResult(result, expected);
        }

        [TestCase(2)]
        [TestCase(58)]
        public void Shift_Two_Test(int shift)
        {
            var arr = new char[,]
            {
                { '0', '1', '2', '3', '4', '5', '6', '7' },
                { '8', '9', '0', '1', '2', '3', '4', '5' },
                { '6', '7', '8', '9', '0', '1', '2', '3' },
                { '4', '5', '6', '7', '8', '9', '0', '1' },
                { '2', '3', '4', '5', '6', '7', '8', '9' },
                { '0', '1', '2', '3', '4', '5', '6', '7' },
                { '8', '9', '0', '1', '2', '3', '4', '5' },
            };

            var result = _shiftService.Shift(arr, shift);

            var expected = new char[,]
            {
                { '4', '5', '0', '1', '2', '3', '4', '5' },
                { '6', '7', '8', '9', '0', '1', '2', '3' },
                { '4', '5', '6', '7', '8', '9', '0', '1' },
                { '2', '3', '4', '5', '6', '7', '8', '9' },
                { '0', '1', '2', '3', '4', '5', '6', '7' },
                { '8', '9', '0', '1', '2', '3', '4', '5' },
                { '6', '7', '8', '9', '0', '1', '2', '3' },
            };

            AssertResult(result, expected);
        }

        [TestCase(7)]
        [TestCase(63)]
        [TestCase(119)]
        public void Shift_Seven_Test(int shift)
        {
            var arr = new char[,]
            {
                { '0', '1', '2', '3', '4', '5', '6', '7' },
                { '8', '9', '0', '1', '2', '3', '4', '5' },
                { '6', '7', '8', '9', '0', '1', '2', '3' },
                { '4', '5', '6', '7', '8', '9', '0', '1' },
                { '2', '3', '4', '5', '6', '7', '8', '9' },
                { '0', '1', '2', '3', '4', '5', '6', '7' },
                { '8', '9', '0', '1', '2', '3', '4', '5' },
            };

            var result = _shiftService.Shift(arr, shift);

            var expected = new char[,]
            {
                { '9', '0', '1', '2', '3', '4', '5', '0',  },
                { '1', '2', '3', '4', '5', '6', '7', '8',  },
                { '9', '0', '1', '2', '3', '4', '5', '6',  },
                { '7', '8', '9', '0', '1', '2', '3', '4',  },
                { '5', '6', '7', '8', '9', '0', '1', '2',  },
                { '3', '4', '5', '6', '7', '8', '9', '0',  },
                { '1', '2', '3', '4', '5', '6', '7', '8',  },
            };

            AssertResult(result, expected);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(19)]
        public void Shift_One_Test_WithThreeByThree(int shift)
        {
            var arr = new char[,]
            {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' },
            };

            var expected = new char[,]
            {
                { '9', '1', '2' },
                { '3', '4', '5' },
                { '6', '7', '8' },
            };

            var actual = _shiftService.Shift(arr, shift);
            AssertResult(expected, actual);
        }

        [TestCase(4)]
        [TestCase(13)]
        [TestCase(22)]
        [TestCase(-5)]
        [TestCase(-14)]
        public void Shift_Four_Test_WithThreeByThree(int shift)
        {
            var arr = new char[,]
            {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' },
            };

            var expected = new char[,]
            {
                { '6', '7', '8' },
                { '9', '1', '2' },
                { '3', '4', '5' },
            };

            var actual = _shiftService.Shift(arr, shift);
            AssertResult(expected, actual);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(19)]
        public void Shift_One_Test_WithOneByThree(int shift)
        {
            var arr = new char[,]
            {
                { '1', '2', '3' }
            };

            var expected = new char[,]
            {
                { '3', '1', '2' }
            };

            var actual = _shiftService.Shift(arr, shift);
            AssertResult(expected, actual);
        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-19)]
        public void Shift_MinusOne_Test_WithOneByThree(int shift)
        {
            var arr = new char[,]
            {
                { '1', '2', '3' }
            };

            var expected = new char[,]
            {
                { '2', '3', '1' }
            };

            var actual = _shiftService.Shift(arr, shift);
            AssertResult(expected, actual);
        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-19)]
        [TestCase(8)]
        [TestCase(17)]
        [TestCase(26)]
        public void Shift_MinusOne_Test_WithThreeByThree(int shift)
        {
            var arr = new char[,]
            {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' },
            };

            var expected = new char[,]
            {
                { '2', '3', '4' },
                { '5', '6', '7' },
                { '8', '9', '1' },
            };

            var actual = _shiftService.Shift(arr, shift);
            AssertResult(expected, actual);
        }
    }
}
