using System.Data;

namespace Blog.Application.Providers.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
