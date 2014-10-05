namespace MyFram.Core.Domain
{
    using System;
    using System.Reflection;
    using Attribute.Schema;
    using Validator;

    public abstract partial class BaseEntity
    {
        public abstract string GetPluralName();

        public abstract string GetSingleName();

        public Tuple<bool, string> IsValid()
        {
            return IsSchemaValid();
        }

        private string CallValidationMethod(MethodInfo method, object instance)
        {
            var message = string.Empty;

            if (method != null && instance != null)
            {
                var methodReturn = (Tuple<bool, string>)method.Invoke(instance, null);

                if (!methodReturn.Item1)
                {
                    message += methodReturn.Item2;
                }
            }

            return message;
        }

        private string ExecuteValidator()
        {
            var message = string.Empty;

            if (Validator != null)
            {
                var validationMethod = typeof(BaseValidator<>).MakeGenericType(GetType()).GetMethod("Validate");
                message += CallValidationMethod(validationMethod, Validator);
            }

            return message;
        }

        private Tuple<bool, string> IsSchemaValid()
        {
            var message = ValidateSchemaAttributes();
            message += ExecuteValidator();

            var isSchemaValid = string.IsNullOrEmpty(message);
            return new Tuple<bool, string>(isSchemaValid, message);
        }

        private string ValidateSchemaAttributes()
        {
            var message = string.Empty;

            foreach (var property in GetType().GetProperties())
            {
                if (property.PropertyType.BaseType == typeof(BaseEntity))
                {
                    var childInstance = property.GetValue(this, null);

                    if (childInstance != null)
                    {
                        var childIsSchemaValid = property.PropertyType.GetMethod("IsSchemaValid");
                        message += CallValidationMethod(childIsSchemaValid, childInstance);
                    }
                }
                else
                {
                    foreach (var attribute in property.GetCustomAttributes(true))
                    {
                        if (attribute.GetType().BaseType == typeof(BaseSchemaAttribute))
                        {
                            var attributeIsSchemaValid = attribute.GetType().GetMethod("Validate");
                            message += CallValidationMethod(attributeIsSchemaValid, attribute);
                        }
                    }
                }
            }

            return message;
        }
    }
}