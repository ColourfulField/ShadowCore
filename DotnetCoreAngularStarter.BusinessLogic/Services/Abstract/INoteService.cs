using System;
using System.Threading.Tasks;
using DotnetCoreAngularStarter.Models.DTO;
using ShadowBox.AutomaticDI.Interfaces;

namespace DotnetCoreAngularStarter.BusinessLogic.Services.Abstract
{
    public interface INoteService : IScopedLifetime
    {
        Task<string> GetString();
        Task<NoteDTO> GetNote();
        Task<Guid> AddNote(string text);
    }
}
