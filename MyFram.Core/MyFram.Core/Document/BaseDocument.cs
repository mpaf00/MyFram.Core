namespace MyFram.Core.Document
{
    using System;
    using Util;

    public abstract class BaseDocument
    {
        protected string Mask { get; set; }

        public virtual Tuple<bool, string> ApplyMask(string item)
        {
            string message;
            var result = true;

            try
            {
                var isValid = Validate(item);

                if (isValid.Item1)
                {
                    var itemMaskared = item.ApplyMask(Mask);
                    message = itemMaskared;
                }
                else
                {
                    throw new Exception(isValid.Item2);
                }
            }
            catch (Exception e)
            {
                result = false;
                message = e.Message;
            }

            return new Tuple<bool, string>(result, message);
        }

        public abstract Tuple<bool, string> Generate();

        public virtual Tuple<bool, string> RemoveMask(string item)
        {
            string message;
            var result = true;

            try
            {
                var itemWithoutMask = item.RemoveMask();
                message = itemWithoutMask;
            }
            catch (Exception e)
            {
                result = false;
                message = e.Message;
            }

            return new Tuple<bool, string>(result, message);
        }

        public abstract Tuple<bool, string> Validate(string item);
    }
}