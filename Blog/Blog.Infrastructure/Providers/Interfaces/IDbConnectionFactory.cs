using System.Data;

namespace Blog.Infrastructure.Providers.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
