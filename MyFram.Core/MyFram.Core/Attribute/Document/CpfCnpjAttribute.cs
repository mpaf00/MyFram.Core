namespace MyFram.Core.Attribute.Document
{
    using System;
    using System.Reflection;
    using Core.Document;
    using Domain;
    using Enum;
    using Reflection;
    using Util;

    public class CpfCnpjAttribute : BaseDocumentAttribute
    {
        public CpfCnpjAttribute(CpfCnpjEnum cpfCnpj)
        {
            CpfCnpj = cpfCnpj;

            switch (CpfCnpj)
            {
                case CpfCnpjEnum.Cpf:
                    {
                        Document = new CpfDocument();
                        break;
                    }
                case CpfCnpjEnum.Cnpj:
                    {
                        Document = new CnpjDocument();
                        break;
                    }
            }
        }

        private CpfCnpjEnum CpfCnpj { get; set; }

        public override string Apply(PropertyInfo property, BaseEntity entity)
        {
            if (Document != null)
            {
                return base.Apply(property, entity);
            }

            var message = string.Empty;
            var value = ReflectionHelper.GetValue(property, entity);

            if (value != null)
            {
                if (property.PropertyType == typeof(string))
                {
                    Document = new CpfDocument();
                    var result = Document.ApplyMask(value.ToString());

                    if (!result.Item1)
                    {
                        Document = new CnpjDocument();
                        result = Document.ApplyMask(value.ToString());
                    }

                    message = result.Item2;
                }
            }
            else
            {
                message = Message.InvalidDocument;
            }

            return message;
        }

        public override Tuple<bool, string> Validate(PropertyInfo property, BaseEntity entity)
        {
            if (Document != null)
            {
                return base.Validate(property, entity);
            }

            var message = string.Empty;
            var value = ReflectionHelper.GetValue(property, entity);

            if (value != null)
            {
                if (property.PropertyType == typeof(string))
                {
                    Document = new CpfDocument();
                    var result = Document.Validate(value.ToString());

                    if (!result.Item1)
                    {
                        Document = new CnpjDocument();
                        result = Document.Validate(value.ToString());
                    }

                    message = result.Item2;
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