namespace MyFram.Core.Document
{
    using System;
    using Util;

    public class CepDocument : BaseDocument
    {
        public CepDocument()
        {
            Mask = "#####-###";
        }

        public override Tuple<bool, string> Generate()
        {
            return new Tuple<bool, string>(false, Message.InvalidMethod);
        }

        public override Tuple<bool, string> Validate(string item)
        {
            var message = item;
            var result = true;

            try
            {
                var itemWithoutMask = item.RemoveMask();
                var isValid = itemWithoutMask.Length == 8;

                if (!isValid)
                {
                    throw new Exception(Message.InvalidDocument);
                }
            }
            catch (Exception e)
            {
                result = false;
                message = e.Message;
            }

            return new Tuple<bool, string>(result, message);
        }
    }
}