namespace MyFram.Core.Attribute.Schema
{
    using System;
    using System.Reflection;
    using Domain;
    using Reflection;

    public class RequiredAttribute : BaseSchemaAttribute
    {
        public override Tuple<bool, string> Validate(PropertyInfo property, BaseEntity entity)
        {
            var isValid = true;
            var message = string.Empty;
            var value = ReflectionHelper.GetValue(property, entity);

            if (value != null)
            {
                switch (property.PropertyType.Name)
                {
                    case "DateTime":
                        {
                            isValid = DateTime.Compare(Convert.ToDateTime(value), new DateTime()) != 0;
                            break;
                        }
                    case "Int32":
                        {
                            isValid = Convert.ToInt32(value) > 0;
                            break;
                        }
                    case "String":
                        {
                            isValid = !string.IsNullOrEmpty(value.ToString());
                            break;
                        }
                    default:
                        {
                            break;
                        }
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