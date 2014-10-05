namespace MyFram.Core.Attribute.Document
{
    using System;
    using System.Reflection;
    using Core.Document;
    using Domain;
    using Reflection;

    public class InscricaoEstadualAttribute : BaseDocumentAttribute
    {
        public InscricaoEstadualAttribute(string namePropertySiglaEstado)
        {
            NamePropertySiglaEstado = namePropertySiglaEstado;
        }

        private string NamePropertySiglaEstado { get; set; }

        public override string Apply(PropertyInfo property, BaseEntity entity)
        {
            Document = new InscricaoEstadualDocument(GetSiglaEstado(entity, NamePropertySiglaEstado));
            return base.Apply(property, entity);
        }

        public override string Remove(PropertyInfo property, BaseEntity entity)
        {
            Document = new InscricaoEstadualDocument(GetSiglaEstado(entity, NamePropertySiglaEstado));
            return base.Remove(property, entity);
        }

        public override Tuple<bool, string> Validate(PropertyInfo property, BaseEntity entity)
        {
            Document = new InscricaoEstadualDocument(GetSiglaEstado(entity, NamePropertySiglaEstado));
            return base.Validate(property, entity);
        }

        private string GetSiglaEstado(BaseEntity entity, string namePropertySiglaEstado)
        {
            var result = string.Empty;
            const string delimiter = ".";

            if (!string.IsNullOrEmpty(namePropertySiglaEstado) && entity != null)
            {
                if (!namePropertySiglaEstado.Contains(delimiter))
                {
                    var value = ReflectionHelper.GetValue(namePropertySiglaEstado, entity);
                    result = value != null ? value.ToString() : string.Empty;
                }
                else
                {
                    var nameProperties = namePropertySiglaEstado.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                    var nameChildProperty = nameProperties[0];
                    var value = ReflectionHelper.GetValue(nameChildProperty, entity);
                    result = GetSiglaEstado(value as BaseEntity, namePropertySiglaEstado.Substring(namePropertySiglaEstado.IndexOf(delimiter, StringComparison.Ordinal) + 1));
                }
            }

            return result;
        }
    }
}