using System.Threading.Tasks;
using ShadowBox.Mapper.Abstract;
using DotnetCoreAngularStarter.Models.EntityFramework.Domain;
using DotnetCoreAngularStarter.Models.DTO;

namespace DotnetCoreAngularStarter.Mappers.BL_DL
{
    public class NoteMappingDtoToVm : AsyncMapping<NoteDTO, Note>
    {
        private readonly IAutoMapService _autoMapService;

        public NoteMappingDtoToVm(IAutoMapService autoMapService)
        {
            _autoMapService = autoMapService;
        }

        protected override async Task MapFieldsAsync(NoteDTO source, Note destination)
        {
            await _autoMapService.AutoMap(source, destination, maxDepth: 0);
        }
    }

    public class NoteMappingVmToDto : AsyncMapping<Note, NoteDTO>
    {
        private readonly IAutoMapService _autoMapService;

        public NoteMappingVmToDto(IAutoMapService autoMapService)
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
