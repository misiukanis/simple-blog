using Blog.Domain.Entities.PostAggregate;
using Blog.Domain.ValueObjects;
using Blog.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Persistence
{
    public static class DatabaseInitializer
    {
        private const string UserEmail = "test@test.com";
        private const string UserPassword = "Test123*";
        private const string RoleName = "administrator";

        public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
        {
            ApplicationDbContext dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();

            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(RoleName))
            {
                await roleManager.CreateAsync(new IdentityRole(RoleName));
            }

            if (await userManager.FindByEmailAsync(UserEmail) == null)
            {
                ApplicationUser user = new ApplicationUser { UserName = UserEmail, Email = UserEmail, EmailConfirmed = true };
                await userManager.CreateAsync(user, UserPassword);
                await userManager.AddToRoleAsync(user, RoleName);
            }

            
            if (await dbContext.Posts.AnyAsync())
            {
                return; // DB has been seeded
            }

            List<Post> posts = new List<Post>
            {
                new Post(Guid.NewGuid(), "Title 1", "Test Introduction 1", "<strong>Content 1</strong>"),
                new Post(Guid.NewGuid(), "Title 2", "Test Introduction 2", "<strong>Content 2</strong>"),
                new Post(Guid.NewGuid(), "Title 3", "Test Introduction 3", "<strong>Content 3</strong>"),
                new Post(Guid.NewGuid(), "Title 4", "Test Introduction 4", "<strong>Content 4</strong>"),
                new Post(Guid.NewGuid(), "Title 5", "Test Introduction 5", "<strong>Content 5</strong>"),
                new Post(Guid.NewGuid(), "Title 6", "Test Introduction 6", "<strong>Content 6</strong>"),
                new Post(Guid.NewGuid(), "Title 7", "Test Introduction 7", "<strong>Content 7</strong>"),
                new Post(Guid.NewGuid(), "Title 8", "Test Introduction 8", "<strong>Content 8</strong>"),
                new Post(Guid.NewGuid(), "Title 9", "Test Introduction 9", "<strong>Content 9</strong>"),
                new Post(Guid.NewGuid(), "Title 10", "Test Introduction 10", "<strong>Test Content 10</strong>"),
                new Post(Guid.NewGuid(), "Title 11", "Test Introduction 11", "<strong>Test Content 11</strong>"),
                new Post(Guid.NewGuid(), "Title 12", "Test Introduction 12", "<strong>Test Content 12</strong>"),
                new Post(Guid.NewGuid(), "Title 13", "Test Introduction 13", "<strong>Test Content 13</strong>"),
                new Post(Guid.NewGuid(), "Title 14", "Test Introduction 14", "<strong>Test Content 14</strong>"),
                new Post(Guid.NewGuid(), "Title 15", "Test Introduction 15", "<strong>Test Content 15</strong>"),
                new Post(Guid.NewGuid(), "Title 16", "Test Introduction 16", "<strong>Test Content 16</strong>"),
                new Post(Guid.NewGuid(), "Title 17", "Test Introduction 17", "<strong>Test Content 17</strong>"),
                new Post(Guid.NewGuid(), "Title 18", "Test Introduction 18", "<strong>Test Content 18</strong>"),
                new Post(Guid.NewGuid(), "Title 19", "Test Introduction 19", "<strong>Test Content 19</strong>"),
                new Post(Guid.NewGuid(), "Title 20", "Test Introduction 20", "<strong>Test Content 20</strong>"),
                new Post(Guid.NewGuid(), "Title 21", "Test Introduction 21", "<strong>Test Content 21</strong>"),
                new Post(Guid.NewGuid(), "Title 22", "Test Introduction 22", "<strong>Test Content 22</strong>"),
                new Post(Guid.NewGuid(), "Title 23", "Test Introduction 23", "<strong>Test Content 23</strong>"),
                new Post(Guid.NewGuid(), "Title 24", "Test Introduction 24", "<strong>Test Content 24</strong>")
            };

            posts[0].AddComment(Guid.NewGuid(), new Author("Autor 1", "author1@email.com"), "Comment 1");
            posts[1].AddComment(Guid.NewGuid(), new Author("Autor 2", "author2@email.com"), "Comment 2");
            posts[2].AddComment(Guid.NewGuid(), new Author("Autor 3", "author3@email.com"), "Comment 3");
            posts[3].AddComment(Guid.NewGuid(), new Author("Autor 4", "author4@email.com"), "Comment 4");
            posts[4].AddComment(Guid.NewGuid(), new Author("Autor 5", "author5@email.com"), "Comment 5");
            posts[5].AddComment(Guid.NewGuid(), new Author("Autor 6", "author6@email.com"), "Comment 6");
            posts[6].AddComment(Guid.NewGuid(), new Author("Autor 7", "author7@email.com"), "Comment 7");
            posts[7].AddComment(Guid.NewGuid(), new Author("Autor 8", "author8@email.com"), "Comment 8");
            posts[8].AddComment(Guid.NewGuid(), new Author("Autor 9", "author9@email.com"), "Comment 9");
            posts[9].AddComment(Guid.NewGuid(), new Author("Autor 10", "author10@email.com"), "Comment 10");
            posts[11].AddComment(Guid.NewGuid(), new Author("Autor 11", "author11@email.com"), "Comment 11");
            posts[11].AddComment(Guid.NewGuid(), new Author("Autor 12", "author12@email.com"), "Comment 12");
            posts[11].AddComment(Guid.NewGuid(), new Author("Autor 13", "author13@email.com"), "Comment 13");
            posts[11].AddComment(Guid.NewGuid(), new Author("Autor 14", "author14@email.com"), "Comment 14");
            posts[11].AddComment(Guid.NewGuid(), new Author("Autor 15", "author15@email.com"), "Comment 15");

            dbContext.Posts.AddRange(posts);

            await dbContext.SaveChangesAsync();            
        }
    }
}
