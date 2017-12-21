using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShadowCore.Models.DTO;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowCore.BusinessLogic.Services.Abstract
{
    public interface INoteService : IScopedLifetime
    {
        Task<string> GetString();
        Task<NoteDTO> GetNote();
        Task<Guid> AddNote(string text);
        Task<IList<NoteDTO>> GetNotesByText(string text);
    }
}
