using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEveryDay.Common
{
    //String class extension
    public static class StringExtensions
    {
        #region Methods
        public static string Base64Encode(this string input)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(input));
        }
        public static string Base64Decode(this string input)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(input));
        }

        public static String Join<T>(this IEnumerable<T> enumerable, string seperator)
        {
            var nullRepresentation = "";
            var enumerableAsStrings = enumerable.Select(a => a == null ? nullRepresentation : a.ToString()).ToArray();
            return string.Join(seperator, enumerableAsStrings);
        }

        public static String Join<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Join(", ");
        }

        #endregion
    }
}
