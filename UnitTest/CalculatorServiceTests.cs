using ArrayShiftService;
using Moq;

namespace UnitTest
{
    public class CalculatorServiceTests : TestBase
    {
        private ShiftService _shiftService;
        private Mock<IShiftCalculatorService> _mockCalculatorService;
        private char[,] _actualResult;

        [SetUp]
        public void Setup()
        {
            var calculatorService = new ShiftCalculatorService();
            _mockCalculatorService = new Mock<IShiftCalculatorService>();
            _mockCalculatorService
                .Setup(x => x.Calculate(It.IsAny<char[,]>(), It.IsAny<int>()))
                .Callback((char[,] source, int shift) =>
                {
                    _actualResult = calculatorService.Calculate(source, shift);
                });

            _shiftService = new ShiftService(_mockCalculatorService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockCalculatorService.Verify(x => x.Calculate(It.IsAny<char[,]>(), It.IsAny<int>()), Times.Once);
            _mockCalculatorService.VerifyNoOtherCalls();
        }

        [TestCase(1)]
        [TestCase(57)]
        public void Shift_One_Test(int shift)
        {
            // Arrange
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

            // Act
            _shiftService.Shift(arr, shift);

            // Assert
            AssertResult(expected, _actualResult);
        }

        [TestCase(2)]
        [TestCase(58)]
        public void Shift_Two_Test(int shift)
        {
            // Arrange
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

            // Act
            _shiftService.Shift(arr, shift);

            // Assert
            AssertResult(expected, _actualResult);
        }

        [TestCase(7)]
        [TestCase(63)]
        [TestCase(119)]
        public void Shift_Seven_Test(int shift)
        {
            // Arrange
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

            // Act
            _shiftService.Shift(arr, shift);

            // Assert
            AssertResult(expected, _actualResult);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(19)]
        public void Shift_One_Test_WithThreeByThree(int shift)
        {
            // Arrange
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

            // Act
            _shiftService.Shift(arr, shift);

            // Assert
            AssertResult(expected, _actualResult);
        }

        [TestCase(4)]
        [TestCase(13)]
        [TestCase(22)]
        [TestCase(-5)]
        [TestCase(-14)]
        public void Shift_Four_Test_WithThreeByThree(int shift)
        {
            // Arrange
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

            // Act
            _shiftService.Shift(arr, shift);

            // Assert
            AssertResult(expected, _actualResult);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(19)]
        public void Shift_One_Test_WithOneByThree(int shift)
        {
            // Arrange
            var arr = new char[,]
            {
                { '1', '2', '3' }
            };

            var expected = new char[,]
            {
                { '3', '1', '2' }
            };

            // Act
            _shiftService.Shift(arr, shift);

            // Assert
            AssertResult(expected, _actualResult);
        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-19)]
        public void Shift_MinusOne_Test_WithOneByThree(int shift)
        {
            // Arrange
            var arr = new char[,]
            {
                { '1', '2', '3' }
            };

            var expected = new char[,]
            {
                { '2', '3', '1' }
            };

            // Act
            _shiftService.Shift(arr, shift);

            // Assert
            AssertResult(expected, _actualResult);
        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-19)]
        [TestCase(8)]
        [TestCase(17)]
        [TestCase(26)]
        public void Shift_MinusOne_Test_WithThreeByThree(int shift)
        {
            // Arrange
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

            // Act
            _shiftService.Shift(arr, shift);

            // Assert
            AssertResult(expected, _actualResult);
        }
    }
}
