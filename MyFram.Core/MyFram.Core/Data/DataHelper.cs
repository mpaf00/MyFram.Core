namespace MyFram.Core.Data
{
    using Domain;

    public abstract class DataHelper<T> where T : BaseEntity
    {
        public static string NameDatabase { get; set; }

        protected static string HostName { get; set; }

        protected static string NameConnectionString { get; set; }

        protected static string Port { get; set; }
    }
}