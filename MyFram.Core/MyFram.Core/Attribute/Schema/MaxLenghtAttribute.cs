namespace MyFram.Core.Attribute.Schema
{
    using System;
    using System.Reflection;
    using Domain;
    using Reflection;

    public class MaxLenghtAttribute : BaseSchemaAttribute
    {
        public MaxLenghtAttribute(int maxLenght)
        {
            MaxLenght = maxLenght;
        }

        public int MaxLenght { get; set; }

        public override Tuple<bool, string> Validate(PropertyInfo property, BaseEntity entity)
        {
            var isValid = true;
            var message = string.Empty;
            var value = ReflectionHelper.GetValue(property, entity);

            if (value != null)
            {
                isValid = value.ToString().Length > MaxLenght;
            }

            if (!isValid)
            {
                message += string.Empty;
            }

            return new Tuple<bool, string>(isValid, message);
        }
    }
}