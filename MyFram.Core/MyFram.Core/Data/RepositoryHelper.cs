namespace MyFram.Core.Data
{
    using System;
    using Contract;
    using Domain;
    using Ninject;
    using Ninject.Modules;
    using Util;

    public static class RepositoryHelper<T> where T : BaseEntity
    {
        public static IRepository<T> GetRepository()
        {
            var dataBinding = Activator.CreateInstance(Config.DataBinding<T>());

            using (var kernel = new StandardKernel(dataBinding as NinjectModule))
            {
                var repository = kernel.Get<IRepository<T>>();

                return repository;
            }
        }
    }
}