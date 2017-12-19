using System;

namespace DotnetCoreAngularStarter.Models.DTO
{
    public class NoteDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int Number { get; set; }
        public NoteDTO ParentNote { get; set; }
    }
}
