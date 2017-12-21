using System.Linq;
using ShadowCore.Common;
using ShadowCore.Models.EntityFramework.Abstract;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowBox.AutomaticDI;

namespace ShadowCore.DAL.EntityFramework.Repository
{
    [Feature(DependencyInjectionFeatureNames.EntityFramework)]
    public class NoteRepository: BaseRepository<Note>
    {
        public NoteRepository(IShadowCoreDbContext context): base(context)
        {
        }

        public IQueryable<Note> GetByTextAsync(string text)
        {
            return _db.Set<Note>().Where(x => x.Text.Contains(text));
        }
    }
}
