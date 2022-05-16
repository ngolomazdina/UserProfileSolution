using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfile.Common.Extentions
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumDisplayAttribute : Attribute
    {
        private string _displayAttribute;

        public EnumDisplayAttribute(string displayAttribute)
        {
            _displayAttribute = displayAttribute;
        }

        public string DisplayAttribute
        {
            get { return _displayAttribute; }
        }
    }

    public static class EnumExtensions
    {
        public static string ToDisplayString(this Enum value)
        {
            var attribute = (EnumDisplayAttribute)
                Attribute.GetCustomAttribute(
                                value.GetType().GetField(value.ToString()),
                                typeof(EnumDisplayAttribute));

            if (attribute == null) return value.ToString();
            else return attribute.DisplayAttribute;
        }
    }
}
