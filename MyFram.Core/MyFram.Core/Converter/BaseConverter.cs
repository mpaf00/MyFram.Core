namespace MyFram.Core.Converter
{
    using System;
    using Exception;

    public abstract class BaseConverter<TOrigin, TDestination>
    {
        public void Map()
        {
            try
            {
                BeforeMap();
                Mapping();
                AfterMap();
            }
            catch (Exception e)
            {
                ExceptionHelper.DealingException(e);
            }
        }

        public TDestination ToDestination(TOrigin item)
        {
            try
            {
                BeforeToDestination(item);
                var objectReturn = ConvertToDestination(item);
                AfterToDestination(objectReturn);

                return objectReturn;
            }
            catch (Exception e)
            {
                ExceptionHelper.DealingException(e);
                throw;
            }
        }

        public TOrigin ToOrigin(TDestination item)
        {
            try
            {
                BeforeToOrigin(item);
                var objectReturn = ConvertToOrigin(item);
                AfterToOrigin(objectReturn);

                return objectReturn;
            }
            catch (Exception e)
            {
                ExceptionHelper.DealingException(e);
                throw;
            }
        }

        protected virtual void AfterMap()
        {
        }

        protected virtual void AfterToDestination(TDestination objectReturn)
        {
        }

        protected virtual void AfterToOrigin(TOrigin objectReturn)
        {
        }

        protected virtual void BeforeMap()
        {
        }

        protected virtual void BeforeToDestination(TOrigin item)
        {
        }

        protected virtual void BeforeToOrigin(TDestination item)
        {
        }

        protected abstract TDestination ConvertToDestination(TOrigin item);

        protected abstract TOrigin ConvertToOrigin(TDestination item);

        protected abstract void Mapping();
    }
}