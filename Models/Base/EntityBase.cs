using Models.Tools;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Base
{
    public abstract class EntityBase : object
    {
        public EntityBase() : base()
        {
            Id = System.Guid.NewGuid();
            InsertDateTime = Utility.Now;
        }

        // **********
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Id))]
        public System.Guid Id { get; set; }
        // **********

        // **********
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.InsertDateTime))]
        public System.DateTime InsertDateTime { get; set; }
        // **********
        public DateTime UpdateDateTime { get; set; }
        public DateTime DeleteDateTime { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
