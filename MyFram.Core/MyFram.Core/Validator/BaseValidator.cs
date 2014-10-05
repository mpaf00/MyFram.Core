namespace MyFram.Core.Validator
{
    using System;
    using Domain;
    using FluentValidation;

    public abstract class BaseValidator<T> : AbstractValidator<T> where T : BaseEntity
    {
        public abstract Tuple<bool, string> Validate();
    }
}