namespace ViewModels.Base
{
    public class PersonViewModelBase : ViewModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }

        public string FullName
        {
            get
            {
                string fullName = (FirstName.Trim() + " " + LastName.Trim()).Trim();

                return fullName;
            }
        }
    }
}
