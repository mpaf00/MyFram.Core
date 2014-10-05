namespace MyFram.Core.Attribute.Schema
{
    using System;
    using System.Reflection;
    using Domain;
    using Reflection;

    public class RangeAttribute : BaseSchemaAttribute
    {
        public RangeAttribute(int start, int end)
        {
            End = end;
            Start = start;
        }

        public int End { get; set; }

        public int Start { get; set; }

        public override Tuple<bool, string> Validate(PropertyInfo property, BaseEntity entity)
        {
            var isValid = true;
            var message = string.Empty;

            if (property != null && property.PropertyType == typeof(int))
            {
                var value = ReflectionHelper.GetValue(property, entity);

                if (value != null)
                {
                    var intValue = Convert.ToInt32(value);
                    isValid = (intValue >= Start && intValue <= End);
                }
            }

            if (!isValid)
            {
                message += string.Empty;
            }

            return new Tuple<bool, string>(isValid, message);
        }
    }
}