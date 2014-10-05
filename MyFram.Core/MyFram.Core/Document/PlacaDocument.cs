namespace MyFram.Core.Document
{
    using System;
    using System.Linq;
    using Util;

    public class PlacaDocument : BaseDocument
    {
        public PlacaDocument()
        {
            Mask = "###-####";
        }

        public override Tuple<bool, string> Generate()
        {
            string message;
            var result = true;

            try
            {
                var random = new Random();

                var n1 = (char)random.Next('A', 'Z' + 1);
                var n2 = (char)random.Next('A', 'Z' + 1);
                var n3 = (char)random.Next('A', 'Z' + 1);
                var n4 = random.Next(10);
                var n5 = random.Next(10);
                var n6 = random.Next(10);
                var n7 = random.Next(10);

                var item = string.Format("{0}{1}{2}{3}{4}{5}{6}", n1, n2, n3, n4, n5, n6, n7);
                message = item.ApplyMask(Mask);
            }
            catch (Exception e)
            {
                result = false;
                message = e.Message;
            }

            return new Tuple<bool, string>(result, message);
        }

        public override Tuple<bool, string> Validate(string item)
        {
            var message = item;
            var result = true;

            try
            {
                var itemWithoutMask = item.RemoveMask();
                var isValid = itemWithoutMask.Length == 7 &&
                      itemWithoutMask.Substring(0, 3).ToList().All(char.IsLetter) &&
                      itemWithoutMask.Substring(3, itemWithoutMask.Length - 3).All(char.IsDigit);

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