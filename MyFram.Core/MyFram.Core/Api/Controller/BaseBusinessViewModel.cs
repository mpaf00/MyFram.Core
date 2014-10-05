namespace MyFram.Core.Api.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Script.Serialization;
    using Core.Domain;
    using Core.Domain.Business;
    using Data;
    using Data.Contract;
    using Domain;
    using Exception;
    using Linq.DynamicQuery;
    using Reflection;
    using Util;

    public partial class BaseBusinessController<T> : BaseController where T : BaseBusinessEntity
    {
        public BaseBusinessController()
        {
            try
            {
                Repository = RepositoryHelper<T>.GetRepository();
            }
            catch (Exception e)
            {
                ExceptionHelper.DealingException(e);
            }
        }

        public IRepository<T> Repository { get; set; }

        public ReturnApi GetColumns(string query)
        {
            var returnObject = new ReturnApi
                                   {
                                       IsValid = true,
                                       Message = string.Empty
                                   };

            try
            {
                query = query.Replace("\\", "");

                var queryObject = new JavaScriptSerializer().Deserialize<QueryApi>(query);

                if (queryObject == null)
                {
                    ExceptionHelper.RaiseException(Message.EntityNotFound);
                }

                if (string.IsNullOrEmpty(queryObject.Columns))
                {
                    ExceptionHelper.RaiseException(Message.EntityNotFound);
                }

                if (!string.IsNullOrEmpty(queryObject.Where) && queryObject.Where.ToLower().Contains("handle"))
                {
                    queryObject.Where = queryObject.Where.Replace("\"", "");
                }

                var columns = queryObject.Columns.Split(new[] { queryObject.Delimiter }, StringSplitOptions.RemoveEmptyEntries);
                var data = queryObject.Limit == 0
                               ? Repository.All().AsQueryable()
                                     .Where(queryObject.Where)
                               : Repository.All().AsQueryable().Take(queryObject.Limit)
                                     .Where(queryObject.Where);

                if (!string.IsNullOrEmpty(queryObject.Order))
                {
                    data = data.OrderBy(queryObject.Order);
                }

                var itens = new List<ExpandoObject>();

                foreach (var item in data)
                {
                    dynamic expandoObject = new ExpandoObject();
                    var p = expandoObject as IDictionary<String, object>;

                    foreach (var column in columns)
                    {
                        var firstProperty = column.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0];
                        var property = typeof(T).GetProperties().FirstOrDefault(e => e.Name.ToLower() == firstProperty.ToLower());
                        p[column] = GetColumnValue(property, item, column);
                    }

                    itens.Add(expandoObject);
                }

                returnObject.Data = itens.AsQueryable();
            }
            catch (Exception e)
            {
                returnObject.IsValid = false;
                returnObject.Message = e.Message;
            }

            return returnObject;
        }

        protected object GetEntityComplete(BaseEntity entity)
        {
            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType.IsClass && property.PropertyType.BaseType.Name.Contains("BaseEntity"))
                {
                    var propertyType = property.PropertyType;
                    var nameChildPropertyHandle = "Handle" + property.Name;
                    var handleChildProperty = entity.GetType().GetProperties().FirstOrDefault(e => e.Name.ToLower() == nameChildPropertyHandle.ToLower());
                    var childHandle = (int?)handleChildProperty.GetValue(entity, null);

                    if (propertyType != null && handleChildProperty != null && (childHandle > 0))
                    {
                        var getRepositoryMethod = typeof(RepositoryHelper<>).MakeGenericType(propertyType).GetMethod("GetRepository");

                        if (getRepositoryMethod != null)
                        {
                            dynamic childRepository = getRepositoryMethod.Invoke(null, null);
                            var childItem = childRepository.FindBy(childHandle.ToString());

                            if (childItem != null)
                            {
                                entity.GetType().GetProperty(property.Name).SetValue(entity, GetEntityComplete(childItem), null);
                            }
                        }
                    }
                }
            }

            return entity;
        }

        private string GetColumnValue(PropertyInfo property, BaseEntity entity, string column)
        {
            if (!column.Contains("."))
            {
                return property != null ? ReflectionHelper.GetValue(property, entity).ToString() : string.Empty;
            }

            const string delimiter = ".";
            var properties = column.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
            var nameChildPropertyHandle = "Handle" + properties[0];
            var childProperty = entity.GetType().GetProperties().FirstOrDefault(e => e.Name.ToLower() == properties[0].ToLower());
            var entityChildProperty = childProperty.PropertyType.GetProperties().FirstOrDefault(e => e.Name.ToLower() == properties[1].ToLower());
            var handleChildProperty = entity.GetType().GetProperties().FirstOrDefault(e => e.Name.ToLower() == nameChildPropertyHandle.ToLower());
            var childHandle = (int?)handleChildProperty.GetValue(entity, null);

            if (entityChildProperty != null && handleChildProperty != null && (childHandle > 0))
            {
                var getRepositoryMethod = typeof(RepositoryHelper<>).MakeGenericType(childProperty.PropertyType).GetMethod("GetRepository");

                if (getRepositoryMethod != null)
                {
                    dynamic childRepository = getRepositoryMethod.Invoke(null, null);
                    var childItem = childRepository.FindBy(childHandle.ToString());

                    if (childItem != null)
                    {
                        return GetColumnValue(entityChildProperty, childItem, column.Substring(column.IndexOf(delimiter) + 1));
                    }
                }
            }

            return string.Empty;
        }
    }
}