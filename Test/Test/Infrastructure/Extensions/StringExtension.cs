using System.Collections.Generic;

namespace Test.Infrastructure.Extensions
{
    public static class StringExtension
    {
        public static string Join(this IEnumerable<string> source, string joinValue) => string.Join(joinValue, source);
        
        public static bool HasValue(this string str) => !string.IsNullOrWhiteSpace(str);
    }
}