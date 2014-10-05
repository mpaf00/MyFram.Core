namespace MyFram.Core.Domain.Business
{
    public abstract partial class BaseBusinessEntity : BaseEntity
    {
        public virtual int HandleAgent { get; set; }

        public virtual int HandleCompany { get; set; }

        public virtual int HandleCompanyGroup { get; set; }

        public virtual int HandleUser { get; set; }
    }
}