namespace MyFram.Core.Attribute.Schema
{
    using System;
    using System.Reflection;
    using Domain;
    using Reflection;

    public class NotEmptyAttribute : BaseSchemaAttribute
    {
        public override Tuple<bool, string> Validate(PropertyInfo property, BaseEntity entity)
        {
            var isValid = true;
            var message = string.Empty;

            if (property != null && property.PropertyType == typeof(string))
            {
                var value = ReflectionHelper.GetValue(property, entity);

                if (value != null)
                {
                    isValid = string.IsNullOrEmpty(value.ToString());
                }

                if (!isValid)
                {
                    message += string.Empty;
                }
            }

            return new Tuple<bool, string>(isValid, message);
        }
    }
}