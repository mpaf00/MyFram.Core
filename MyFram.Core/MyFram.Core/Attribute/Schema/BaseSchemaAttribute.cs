namespace MyFram.Core.Attribute.Schema
{
    using System;
    using System.Reflection;
    using Domain;

    public abstract class BaseSchemaAttribute
    {
        public abstract Tuple<bool, string> Validate(PropertyInfo property, BaseEntity entity);
    }
}