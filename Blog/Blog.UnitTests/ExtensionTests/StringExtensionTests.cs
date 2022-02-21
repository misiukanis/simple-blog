using Blog.Common.Extensions;
using Xunit;

namespace Blog.UnitTests.ExtensionTests
{
    public class StringExtensionTests
    {
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
