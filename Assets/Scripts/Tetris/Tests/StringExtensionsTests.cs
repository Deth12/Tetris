using NUnit.Framework;
using Extensions;

namespace Tetris.Tests
{
    public class StringExtensionsTests
    {
        [Test]
        [TestCase(150, ExpectedResult= "000000150")]
        [TestCase(0, ExpectedResult= "000000000")]
        [TestCase(-100, ExpectedResult= "000000100")]
        [TestCase(123456, ExpectedResult= "000123456")]
        [TestCase(123456789, ExpectedResult= "123456789")]
        public string FormatedStringTest(int value)
        {
            return value.ToFormatedScore(9, '0');
        }
    }
}
