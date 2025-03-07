﻿namespace Models.Tools
{
    public static class Utility
    {
        static Utility()
        {
        }

        public static System.DateTime Now
        {
            get
            {
                System.Globalization.CultureInfo currentCulture =
                    System.Threading.Thread.CurrentThread.CurrentCulture;

                System.Globalization.CultureInfo englishCulture =
                    new System.Globalization.CultureInfo(name: "en-US");

                System.Threading.Thread.CurrentThread.CurrentCulture = englishCulture;

                System.DateTime now = System.DateTime.Now;

                System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;

                return now;
            }
        }
    }
}
