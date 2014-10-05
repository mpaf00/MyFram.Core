namespace MyFram.Core.Domain.Enum
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public abstract class BaseEnum<T> where T : BaseEnum<T>
    {
        public readonly string Description;
        public readonly string Key;
        public readonly string Value;
        protected static List<T> Values = new List<T>();

        public BaseEnum(string key, string value, string description)
        {
            Description = description;
            Key = key;
            Value = value;

            Values.Add((T)this);
        }

        public static T GetByKey(string key)
        {
            var objectReturn = Values.FirstOrDefault(x => x.Key == key);
            return objectReturn;
        }

        public override string ToString()
        {
            return Value;
        }

        protected static ReadOnlyCollection<T> GetValues()
        {
            return Values.AsReadOnly();
        }
    }
}