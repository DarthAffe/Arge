using System;
using System.Linq;
using System.Reflection;

namespace Arge.Extensions
{
    public static class EnumExtension
    {
        #region Methods

        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            MemberInfo[] mi = enumVal.GetType().GetMember(enumVal.ToString());
            return (T)mi[0].GetCustomAttributes(typeof(T), true).FirstOrDefault();
        }

        #endregion
    }
}
