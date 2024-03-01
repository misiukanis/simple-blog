namespace Blog.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string entityName, object key, object value) : base($"Entity {entityName} for {key} = {value} was not found") { }
    }
}
