// Extensions/EnumExtensions.cs
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Cancha.Web.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Name ?? enumValue.ToString();
        }

        public static List<(TEnum Value, string DisplayName)> GetEnumList<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => (e, e.GetDisplayName()))
                .ToList();
        }
    }
}