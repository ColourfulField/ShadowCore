using System;

namespace ShadowCore.Models.EntityFramework.Domain
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int Number { get; set; }
        public Guid NoteId { get; set; }
        public virtual Note ParentNote { get; set; }
    }
}
