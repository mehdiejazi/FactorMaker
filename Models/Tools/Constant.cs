namespace Models
{
    /// <summary>
    /// http://63-characters-is-the-longest-possible-domain-name-for-a-website.com/
    /// </summary>
    public static class Constant
    {
        static Constant()
        {
        }

        public static class Length
        {
            static Length()
            {
            }

            // **********
            public const int IP = 45;
            // **********

            // **********
            public const int GUID = 36;
            // **********

            // **********
            public const int TOKEN = 512;
            // **********

            // **********
            /// <summary>
            /// SHA-256
            /// </summary>
            public const int PASSWORD = 64;
            // **********

            // **********
            public const int USERNAME = 20;
            // **********

            // **********
            public const int FIRST_NAME = 30;
            public const int LAST_NAME = 50;
            // **********

            // **********
            public const int SESSION_ID = 80;
            // **********

            // **********
            public const int DOMAIN_NAME = 100;
            // **********

            // **********
            public const int DISPLAY_NAME = 200;
            // **********

            // **********
            public const int GENERAL_NAME = 100;
            // **********

            // **********
            public const int NATIONAL_CODE = 10;
            // **********

            // **********
            public const int EMAIL_ADDRESS = 254;
            public const int URL = 254;
            // **********

            // **********
            public const int CAPTCHA_IMAGE_TEXT = 6;
            // **********

            // **********
            public const int SITE_ABSOLUTE_PATH = 200;
            // **********

            // **********
            public const int MIN_CELL_PHONE_NUMBER = 11;
            // **********

            // **********
            public const int MAX_CELL_PHONE_NUMBER = 14;
            // **********

            // **********
            public const int CELL_PHONE_NUMBER_VERIFICATION_KEY = 10;
            // **********

            public const int DESCRIPTION = 250;
            public const int TEXT = 250;
            public const int FULL_DESCRIPTION = 1000;
            public const int PUBLIC_NAME = 50;
            public const int FILE_NAME = 50;
            public const int VOCHER_CODE = 50;
            public const int BODY = 500;
            public const int VERIFICATION_CODE = 5;
        }
    }
}
