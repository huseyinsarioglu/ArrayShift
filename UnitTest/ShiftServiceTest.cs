using ArrayShiftService;
using Moq;

namespace UnitTest
{
    public class ShiftServiceTest : TestBase
    {
        private ShiftService _shiftService;
        private Mock<IShiftCalculatorService> _mockCalculatorService;

        [SetUp]
        public void Setup()
        {
            _mockCalculatorService = new Mock<IShiftCalculatorService>();
            _shiftService = new ShiftService(_mockCalculatorService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockCalculatorService.Verify(x => x.Calculate(It.IsAny<char[,]>(), It.IsAny<int>()), Times.Never);
            _mockCalculatorService.VerifyNoOtherCalls();
        }

        [Test]
        public void EmptyArray_Should_Return_EmptyArray()
        {
            var arr = new char[0, 0];
            var result = _shiftService.Shift(arr, 1);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void NullArray_Should_Return_EmptyArray()
        {
            var result = _shiftService.Shift<char>(null, 1);
            Assert.That(result, Is.Empty);
        }

        [TestCase(0)]
        [TestCase(9)]
        [TestCase(18)]
        [TestCase(-9)]
        [TestCase(-18)]
        public void Shift_Zero_Test_WithThreeByThree(int shift)
        {
            var arr = new char[,]
            {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' },
            };

            var actual = _shiftService.Shift(arr, shift);
            AssertResult(arr, actual);
        }
    }
}