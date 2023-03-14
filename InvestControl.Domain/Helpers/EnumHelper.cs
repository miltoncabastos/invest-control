
using System;
using System.ComponentModel;
using System.Reflection;
using InvestControl.Domain.Attributes;

namespace InvestControl.Domain.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T enumValue) 
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }
        
        public static string GetCsvNameColumn(this System.Enum Value)
        {
            Type Type = Value.GetType();

            FieldInfo FieldInfo = Type.GetField(Value.ToString());

            CsvNameAttribute Attribute = FieldInfo.GetCustomAttribute(
                typeof(CsvNameAttribute)
            ) as CsvNameAttribute;

            return Attribute.ColumnName;
        }
    }
}