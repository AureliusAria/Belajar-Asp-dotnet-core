using Bloggie.Web.Controllers.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class TagRepositoryImpl : ITagRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public TagRepositoryImpl(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag = await bloggieDbContext.Tags.FindAsync(id);

            if (tag != null)
            {
                bloggieDbContext.Tags.Remove(tag);
                await bloggieDbContext.SaveChangesAsync();

                return tag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await bloggieDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            var exitingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);

            if (exitingTag != null)
            {
                exitingTag.Name = tag.Name;
                exitingTag.DisplayName = tag.DisplayName;

                //Save changes to database can save it
                await bloggieDbContext.SaveChangesAsync();
                return exitingTag;
            }

            return null;
        }
    }
}
