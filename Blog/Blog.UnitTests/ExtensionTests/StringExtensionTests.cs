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
            string result = value.ToSeoUrl();

            // Assert
            Assert.True(result == "a-b-c");
        }

        [Fact] 
        public void TextSubstringTest()
        {
            // Arrange
            string text = "aaa bbb ccc";

            // Act
            string result = text.TextSubstring(6);

            // Assert
            Assert.True(result == "aaa...");
        }
    }
}
