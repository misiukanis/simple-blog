using Blog.HttpClients;
using Blog.Shared.Constants;

namespace Blog
{
    public static class DependencyInjection
    {
        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationUrl = configuration.GetValue<string>($"{SettingConstants.ApplicationUrl}");

            services.AddHttpClient<BlogHttpClient>(c =>
            {
                c.BaseAddress = new Uri(applicationUrl);
            });
        }
    }
}
