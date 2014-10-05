namespace MyFram.Core.Reflection
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Domain;

    public static class ReflectionHelper
    {
        public static object GetValue(PropertyInfo property, BaseEntity entity)
        {
            if (property != null && entity != null)
            {
                var existPropertyInEntity = entity.GetType().GetProperties().Any(x => x.Name == property.Name);

                if (existPropertyInEntity)
                {
                    return property.GetValue(entity, null);
                }
            }

            return null;
        }

        public static object GetValue(string nameProperty, BaseEntity entity)
        {
            if (!String.IsNullOrEmpty(nameProperty) && entity != null)
            {
                var property = entity.GetType().GetProperties().FirstOrDefault(x => x.Name.ToLower() == nameProperty.ToLower());

                if (property != null)
                {
                    return property.GetValue(entity, null);
                }
            }

            return null;
        }
    }
}