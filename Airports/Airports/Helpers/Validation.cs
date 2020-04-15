using System;
using System.Linq;

namespace AirportsApi.Helpers
{
    public static class Validation
    {
        public static bool IsAirportCodeValid(string code)
        {
            return !string.IsNullOrEmpty(code) && code.Length == 3 && code.All(Char.IsLetter);
        }
    }
}
