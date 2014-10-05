namespace MyFram.Core.Attribute.Document.Enum
{
    using System.ComponentModel;

    public enum CpfCnpjEnum
    {
        [Description("Propriedade tem que ser Cpf")]
        Cpf,

        [Description("Propriedade tem que ser Cnpj")]
        Cnpj,

        [Description("Propriedade pode ser Cpf ou Cnpj")]
        CpfCnpj
    }
}