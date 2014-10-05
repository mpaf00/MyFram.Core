namespace MyFram.Core.Domain
{
    public abstract partial class BaseEntity
    {
        public virtual int HandleS { get; set; }

        public virtual object Validator { get; set; }
    }
}