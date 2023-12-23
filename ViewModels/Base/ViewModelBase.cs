using System;

namespace ViewModels.Base
{
    public class ViewModelBase
    {
        public Guid Id { get; set; }
        public DateTime InsertDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime DeleteDateTime { get; set; }
        public bool IsDeleted { get; set; }

    }
}
