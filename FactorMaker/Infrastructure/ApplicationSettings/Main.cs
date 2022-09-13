using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorMaker.Infrastructure.ApplicationSettings
{
    public class Main
    {
        public int TokenExpiresInMinutes { get; set; }
        public string SecretKey { get; set; }
    }
}
