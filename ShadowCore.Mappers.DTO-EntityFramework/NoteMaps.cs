using System.Threading.Tasks;
using ShadowTools.Mapper.Abstract;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowCore.Models.DTO;

namespace ShadowCore.Mappers.BL_DL
{
    public class NoteDTO_NoteVM : AsyncMapping<NoteDTO, Note>
    {
        private readonly IAutoMapService _autoMapService;

        public NoteDTO_NoteVM(IAutoMapService autoMapService)
        {
            _autoMapService = autoMapService;
        }

        protected override async Task MapFieldsAsync(NoteDTO source, Note destination)
        {
            await _autoMapService.AutoMap(source, destination, maxDepth: 0);
        }
    }

    public class NoteVM_NoteDTO : AsyncMapping<Note, NoteDTO>
    {
        private readonly IAutoMapService _autoMapService;

        public NoteVM_NoteDTO(IAutoMapService autoMapService)
        {
            _autoMapService = autoMapService;
        }

        protected override async Task MapFieldsAsync(Note source, NoteDTO destination)
        {
            await _autoMapService.AutoMap(source, destination, 5);
           // await _autoMapService.AutoMap(source, destination, 5, exclude: new List<string> {nameof(NoteDTO.ParentNote)});
        }
    }
}
