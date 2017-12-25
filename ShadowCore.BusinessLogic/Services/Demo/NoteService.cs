using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using ShadowCore.BusinessLogic.Services.Abstract;
using ShadowCore.DAL.EntityFramework.Abstract;
using ShadowCore.Models.DTO;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowTools.Mapper.Abstract;
using ShadowTools.Utilities.Extensions;
using ShadowTools.Utilities.Models;
using ShadowTools.Mapper.Extensions;

namespace ShadowCore.BusinessLogic.Services
{
    public class NoteService : BaseService, INoteService
    {
        private readonly IStringLocalizer<NoteService> _localizer;

        public NoteService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<NoteService> localizer
        ) : base(unitOfWork, mapper)
        {
            _localizer = localizer;
        }

        public Task<string> GetString()
        {
            return Task.FromResult("Hello from NoteService");
        }

        public async Task<NoteDTO> GetNote()
        {
            var note = new Note
            {
                Id = Guid.NewGuid(),
                Number = 111,
                Text = _localizer["Hello"], //"Hello qweqweqwe",
                CreationDate = DateTime.Now.AddDays(-2),
                ModificationDate = DateTime.Now,
                ParentNote = new Note
                {
                    Id = Guid.NewGuid(),
                    Number = 222,
                    Text = "K BYe2",
                    CreationDate = DateTime.Now.AddDays(-14),
                    ModificationDate = DateTime.Now,
                    ParentNote = new Note
                    {
                        Id = Guid.NewGuid(),
                        Number = 333,
                        Text = "K BYe3",
                        CreationDate = DateTime.Now.AddDays(-14),
                        ModificationDate = DateTime.Now,
                        ParentNote = new Note
                        {
                            Id = Guid.NewGuid(),
                            Number = 444,
                            Text = "K BYe4",
                            CreationDate = DateTime.Now.AddDays(-14),
                            ModificationDate = DateTime.Now,
                            ParentNote = null
                        }
                    }
                }
            };


            note.ParentNote.ParentNote = note;
            return await Mapper.Map<Note, NoteDTO>(note);
        }

        public async Task<IList<NoteDTO>> GetNotesByText(string text)
        {
            var notes = UnitOfWork.Repository<Note>()
                                   .GetAll()
                                   .Where(x => x.Text.Contains(text))
                                   .ApplyPagination(new BaseFilter());

            return await Mapper.Map<Note, NoteDTO>(notes).ToListAsync();
        }

        public async Task<Guid> AddNote(string text)
        {
            var note = new Note
            {
                Text = text,
                CreationDate = DateTime.Now
            };

            UnitOfWork.Repository<Note>().Add(note);
            await UnitOfWork.SaveChangesAsync();

            return note.Id;
        }
    }
}