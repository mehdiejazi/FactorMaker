namespace Models.Base
{
    public class PersonBase:EntityBase
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
