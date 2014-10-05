namespace MyFram.Core.Attribute.Business
{
    using System;
    using System.Reflection;
    using Domain.Business;

    public abstract class BaseBusinessAttribute
    {
        public abstract Tuple<bool, string> Validate(object repository, PropertyInfo property, BaseBusinessEntity entity);
    }
}