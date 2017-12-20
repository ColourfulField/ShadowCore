using System.Linq;
using DotnetCoreAngularStarter.Common;
using DotnetCoreAngularStarter.Models.EntityFramework.Abstract;
using DotnetCoreAngularStarter.Models.EntityFramework.Domain;
using ShadowBox.AutomaticDI;

namespace DotnetCoreAngularStarter.DAL.EntityFramework.Repository
{
    [Feature(DependencyInjectionFeatureNames.EntityFramework)]
    public class NoteRepository: BaseRepository<Note>
    {
        public NoteRepository(IDotnetCoreAngularStarterDbContext context): base(context)
        {
        }

        public IQueryable<Note> GetByTextAsync(string text)
        {
            return _db.Set<Note>().Where(x => x.Text.Contains(text));
        }
    }
}
