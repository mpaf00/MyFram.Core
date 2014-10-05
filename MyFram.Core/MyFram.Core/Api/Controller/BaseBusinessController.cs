namespace MyFram.Core.Api.Controller
{
    using System;
    using System.Linq;
    using Attribute.Business;
    using Core.Domain.Business;
    using Domain;
    using Exception;
    using Util;

    public partial class BaseBusinessController<T> : BaseController where T : BaseBusinessEntity
    {
        public virtual ReturnApi Delete(string id)
        {
            var returnObject = new ReturnApi
                                   {
                                       IsValid = true,
                                       Message = string.Empty
                                   };

            try
            {
                var entities = Repository.FilterBy(x => x.HandleS.ToString() == id);

                if (entities.Any())
                {
                    var entity = entities.FirstOrDefault();
                    BeforeDelete(entity);
                    var isDeletable = IsDeletable(entity);

                    if (isDeletable.Item1)
                    {
                        Delete(entity);
                        AfterDelete(entity);
                    }
                    else
                    {
                        ExceptionHelper.RaiseException(isDeletable.Item2);
                    }
                }
                else
                {
                    ExceptionHelper.RaiseException(string.Format(Message.EntityNotFound, typeof(T)));
                }
            }
            catch (Exception e)
            {
                returnObject.IsValid = false;
                returnObject.Message = e.Message;
            }

            return returnObject;
        }

        public virtual ReturnApi Get()
        {
            var returnObject = new ReturnApi
                                   {
                                       IsValid = true,
                                       Message = string.Empty
                                   };

            try
            {
                returnObject.Data = GetAll();
            }
            catch (Exception e)
            {
                returnObject.IsValid = false;
                returnObject.Message = e.Message;
            }

            return returnObject;
        }

        public virtual ReturnApi GetByCompany(string handleCompany)
        {
            var returnObject = new ReturnApi
                                   {
                                       IsValid = true,
                                       Message = string.Empty
                                   };

            try
            {
                returnObject.Data = GetAll().Where(item => item.HandleCompany.ToString() == handleCompany);
            }
            catch (Exception e)
            {
                returnObject.IsValid = false;
                returnObject.Message = e.Message;
            }

            return returnObject;
        }

        public virtual ReturnApi GetByCompanyGroup(string handleCompanyGroup)
        {
            var returnObject = new ReturnApi
                                   {
                                       IsValid = true,
                                       Message = string.Empty
                                   };

            try
            {
                returnObject.Data = GetAll().Where(item => item.HandleCompanyGroup.ToString() == handleCompanyGroup);
            }
            catch (Exception e)
            {
                returnObject.IsValid = false;
                returnObject.Message = e.Message;
            }

            return returnObject;
        }

        public virtual ReturnApi GetById(string id)
        {
            var returnObject = new ReturnApi
                                   {
                                       IsValid = true,
                                       Message = string.Empty
                                   };

            try
            {
                var entities = Repository.FilterBy(entity => entity.HandleS.ToString() == id);

                if (entities.Any())
                {
                    var entity = entities.FirstOrDefault();
                    returnObject.Data = entity;
                }
                else
                {
                    ExceptionHelper.RaiseException(string.Format(Message.EntityNotFound, typeof(T)));
                }
            }
            catch (Exception e)
            {
                returnObject.IsValid = false;
                returnObject.Message = e.Message;
            }

            return returnObject;
        }

        public virtual ReturnApi GetByUser(string handleUser)
        {
            var returnObject = new ReturnApi
                                   {
                                       IsValid = true,
                                       Message = string.Empty
                                   };

            try
            {
                returnObject.Data = GetAll().Where(item => item.HandleUser.ToString() == handleUser);
            }
            catch (Exception e)
            {
                returnObject.IsValid = false;
                returnObject.Message = e.Message;
            }

            return returnObject;
        }

        public virtual ReturnApi Post(T entity)
        {
            var returnObject = new ReturnApi
                                   {
                                       IsValid = true,
                                       Message = string.Empty
                                   };

            try
            {
                if (entity == null)
                {
                    ExceptionHelper.RaiseException(Message.EntityNotFound);
                }

                if (entity.HandleS > 0)
                {
                    return Put(entity);
                }

                BeforeInsert(entity);

                var isValidSchema = IsValidSchema(entity);

                if (isValidSchema.Item1)
                {
                    var isValid = IsValid(entity);
                    if (isValid.Item1)
                    {
                        Insert(entity);
                        AfterInsert(entity);
                    }
                    else
                    {
                        ExceptionHelper.RaiseException(isValid.Item2);
                    }
                }
                else
                {
                    ExceptionHelper.RaiseException(isValidSchema.Item2);
                }
            }
            catch (Exception e)
            {
                returnObject.IsValid = false;
                returnObject.Message = e.Message;
            }

            return returnObject;
        }

        public virtual ReturnApi Put(T entity)
        {
            var returnObject = new ReturnApi
                                   {
                                       IsValid = true,
                                       Message = string.Empty
                                   };

            try
            {
                if (entity == null)
                {
                    ExceptionHelper.RaiseException(Message.EntityNotFound);
                }

                if (entity != null && entity.HandleS == 0)
                {
                    ExceptionHelper.RaiseException(Message.EntityNotFound);
                }

                BeforeUpdate(entity);

                var isValidSchema = IsValidSchema(entity);

                if (isValidSchema.Item1)
                {
                    var isValid = IsValid(entity);
                    if (isValid.Item1)
                    {
                        Update(entity);
                        AfterUpdate(entity);
                    }
                    else
                    {
                        ExceptionHelper.RaiseException(isValid.Item2);
                    }
                }
                else
                {
                    ExceptionHelper.RaiseException(isValidSchema.Item2);
                }
            }
            catch (Exception e)
            {
                returnObject.IsValid = false;
                returnObject.Message = e.Message;
            }

            return returnObject;
        }

        protected virtual void AfterDelete(T entity)
        {
        }

        protected virtual void AfterInsert(T entity)
        {
        }

        protected virtual void AfterUpdate(T entity)
        {
        }

        protected virtual void BeforeDelete(T entity)
        {
        }

        protected virtual void BeforeInsert(T entity)
        {
        }

        protected virtual void BeforeUpdate(T entity)
        {
        }

        protected virtual void Delete(T entity)
        {
            try
            {
                Repository.Delete(entity);
            }
            catch (Exception e)
            {
                ExceptionHelper.DealingException(e);
            }
        }

        protected virtual IQueryable<T> GetAll()
        {
            return Repository.All();
        }

        protected virtual void Insert(T entity)
        {
            try
            {
                Repository.Add(entity);
            }
            catch (Exception e)
            {
                ExceptionHelper.DealingException(e);
            }
        }

        protected virtual Tuple<bool, string> IsDeletable(T entity)
        {
            return new Tuple<bool, string>(true, string.Empty);
        }

        protected virtual Tuple<bool, string> IsValid(T entity)
        {
            var message = string.Empty;

            foreach (var property in GetType().GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (attribute.GetType().BaseType == typeof(BaseBusinessAttribute))
                    {
                        var attributeIsValid = attribute.GetType().GetMethod("Validate");

                        if (attributeIsValid != null)
                        {
                            var methodReturn = (Tuple<bool, string>)attributeIsValid.Invoke(entity, new object[] { Repository, property, entity });

                            if (!methodReturn.Item1)
                            {
                                message += methodReturn.Item2;
                            }
                        }
                    }
                }
            }

            var isValid = string.IsNullOrEmpty(message);

            return new Tuple<bool, string>(isValid, message);
        }

        protected virtual Tuple<bool, string> IsValidSchema(T entity)
        {
            return entity.IsValid();
        }

        protected virtual void Update(T entity)
        {
            try
            {
                Repository.Update(entity);
            }
            catch (Exception e)
            {
                ExceptionHelper.DealingException(e);
            }
        }
    }
}