namespace MyFram.Core.Attribute.Business
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Data.Contract;
    using Domain.Business;
    using Reflection;
    using Util;

    public class UniquePerCompanyGroupAttribute : BaseBusinessAttribute
    {
        public override Tuple<bool, string> Validate(object repository, PropertyInfo property, BaseBusinessEntity entity)
        {
            try
            {
                var message = string.Empty;

                var filterByMethod =
                    typeof(IRepository<>).MakeGenericType(entity.GetType()).GetMethods().FirstOrDefault(
                        x => x.Name == "FilterBy");

                if (filterByMethod != null)
                {
                    Expression<Func<BaseBusinessEntity, bool>> expression = (x => x.HandleAgent == entity.HandleCompanyGroup);

                    var value = ReflectionHelper.GetValue(property, entity);

                    if (value != null)
                    {
                        dynamic itens = filterByMethod.Invoke(repository, new object[] { expression });
                        var where = string.Format("{0} = {1}", property.Name, value);

                        if (itens.Any())
                        {
                            var itensFiltered = itens.Where(where);
                            var item = itensFiltered.FirstOrDefault();
                            if (item != null && item.HandleS != entity.HandleS)
                            {
                                message = Message.ValueAlreadyExist;
                            }
                        }
                    }
                }

                var isValid = string.IsNullOrEmpty(message);
                return new Tuple<bool, string>(isValid, message);
            }
            catch (Exception e)
            {
                return new Tuple<bool, string>(false, e.Message);
            }
        }
    }
}