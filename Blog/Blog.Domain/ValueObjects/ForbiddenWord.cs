using Blog.Domain.Core;

namespace Blog.Domain.ValueObjects
{
    public class ForbiddenWord : ValueObject
    {
        public string Name { get; private set; }


        public ForbiddenWord(string name)
        {
            Name = name;
        }
    }
}
