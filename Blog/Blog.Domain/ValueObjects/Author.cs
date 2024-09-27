using Blog.Domain.Core;

namespace Blog.Domain.ValueObjects
{
    public class Author : ValueObject
    {
        public string Name { get; private set; }
        public string Email { get; private set; }


        public Author(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
