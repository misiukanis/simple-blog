using Blog.Shared.Extensions;

namespace Blog.UnitTests.ExtensionTests
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("A B C")]
        [InlineData("ą b-ć")]
        [InlineData("a< >b. .c")]
        [InlineData("-a---b--c-")]
        public void ToSeoUrlTest(string value)
        {
            // Arrange
            
            // Act
            var result = value.ToSeoUrl();

            // Assert
            Assert.Equal("a-b-c", result);
        }

        [Fact]
        public void TextSubstringTest()
        {
            // Arrange
            var text = "aaa bbb ccc";

            // Act
            var result = text.TextSubstring(6);

            // Assert
            Assert.Equal("aaa...", result);
        }
    }
}
