using Models.Base;
using System;

namespace Models
{
    public class ImageAsset : EntityBase
    {
        public string FileName { get; set; }
        public string Url { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
    }
}