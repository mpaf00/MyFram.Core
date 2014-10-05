namespace MyFram.Core.Attribute.Schema
{
    using System;
    using System.Reflection;
    using Domain;
    using Reflection;

    public class NotNullAttribute : BaseSchemaAttribute
    {
        public override Tuple<bool, string> Validate(PropertyInfo property, BaseEntity entity)
        {
            var message = string.Empty;
            var value = ReflectionHelper.GetValue(property, entity);

            var isValid = value != null;

            if (!isValid)
            {
                message += string.Empty;
            }

            return new Tuple<bool, string>(isValid, message);
        }
    }
}