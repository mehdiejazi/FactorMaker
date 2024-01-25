using System;
using ViewModels.Base;
using ViewModels.User;

namespace ViewModels.PostCategory
{
    public class PostCategoryViewModel : ViewModelBase
    {
        public PostCategoryViewModel() : base()
        {

        }
        public string Name { get; set; }
        public virtual UserViewModel Owner { get; set; }
        public Guid OwnerId { get; set; }
    }
}
