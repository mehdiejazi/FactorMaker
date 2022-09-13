using System.ComponentModel.DataAnnotations;

namespace Models.Enums
{
    public enum RoleType : int
    {
        // **********
        [Display(ResourceType = typeof(Resources.EnumResource),
            Name = nameof(Resources.EnumResource.Customer))]
        User = 100,
        // **********

        // **********
        [Display(ResourceType = typeof(Resources.EnumResource),
            Name = nameof(Resources.EnumResource.Administrator))]
        Administrator = 300,
        // **********
    }
}
