using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using DotnetCoreAngularStarter.BusinessLogic.Services.Abstract;
using DotnetCoreAngularStarter.Models.DTO;
using DotnetCoreAngularStarter.DAL.Abstract;
using DotnetCoreAngularStarter.Models.EntityFramework.Domain;
using ShadowBox.Mapper.Abstract;

namespace DotnetCoreAngularStarter.BusinessLogic.Services
{
    public class NoteService : INoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<NoteService> _localizer;
        public NoteService(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<NoteService> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }
        public Task<string> GetString()
        {
            return Task.FromResult("HEEEEEEY");
        }

        public async Task<NoteDTO> GetNote()
        {
            var note = new Note
            {
                Id = Guid.NewGuid(),
                Number = 111,
                Text = _localizer["Hello"],  //"Hello qweqweqwe",
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
            return await _mapper.Map<Note, NoteDTO>(note);
        }

        public async Task<Guid> AddNote(string text)
        {
            var note = new Note
            {
                Text = text,
                CreationDate = DateTime.Now
            };

            _unitOfWork.Repository<Note>().Add(note);
            await _unitOfWork.SaveChangesAsync();

            return note.Id;
        }
    }
}
