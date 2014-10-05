namespace MyFram.Core.Attribute.Schema
{
    using System;
    using System.Reflection;
    using Domain;
    using Reflection;

    public class LenghtAttribute : BaseSchemaAttribute
    {
        public LenghtAttribute(int lenght)
        {
            Lenght = lenght;
        }

        public int Lenght { get; set; }

        public override Tuple<bool, string> Validate(PropertyInfo property, BaseEntity entity)
        {
            var isValid = true;
            var message = string.Empty;
            var value = ReflectionHelper.GetValue(property, entity);

            if (value != null)
            {
                isValid = value.ToString().Length == Lenght;
            }

            if (!isValid)
            {
                message += string.Empty;
            }

            return new Tuple<bool, string>(isValid, message);
        }
    }
}