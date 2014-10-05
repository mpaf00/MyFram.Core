namespace MyFram.Core.Attribute.Document
{
    using System;
    using System.Reflection;
    using Core.Document;
    using Domain;
    using Reflection;
    using Util;

    public class BaseDocumentAttribute
    {
        protected BaseDocument Document { get; set; }

        public virtual string Apply(PropertyInfo property, BaseEntity entity)
        {
            var valueWithoutMask = string.Empty;

            var value = ReflectionHelper.GetValue(property, entity);

            if (value != null)
            {
                if (property.PropertyType == typeof(string))
                {
                    var result = Document.ApplyMask(value.ToString());
                    valueWithoutMask = result.Item1 ? result.Item2 : value.ToString();
                }
            }

            return valueWithoutMask;
        }

        public virtual string Remove(PropertyInfo property, BaseEntity entity)
        {
            var valueWithoutMask = string.Empty;

            var value = ReflectionHelper.GetValue(property, entity);

            if (value != null)
            {
                if (property.PropertyType == typeof(string))
                {
                    var result = Document.RemoveMask(value.ToString());
                    valueWithoutMask = result.Item1 ? result.Item2 : value.ToString();
                }
            }

            return valueWithoutMask;
        }

        public virtual Tuple<bool, string> Validate(PropertyInfo property, BaseEntity entity)
        {
            var message = string.Empty;
            var value = ReflectionHelper.GetValue(property, entity);

            if (value != null)
            {
                if (property.PropertyType == typeof(string))
                {
                    message = Document.Validate(value.ToString()).Item2;
                }
            }
            else
            {
                message = Message.InvalidDocument;
            }

            var isValid = string.IsNullOrEmpty(message);

            return new Tuple<bool, string>(isValid, message);
        }
    }
}