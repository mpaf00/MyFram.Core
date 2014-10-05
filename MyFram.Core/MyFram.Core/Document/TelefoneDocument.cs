namespace MyFram.Core.Document
{
    using System;
    using Util;

    public class TelefoneDocument : BaseDocument
    {
        public override Tuple<bool, string> ApplyMask(string item)
        {
            string message;
            var result = true;

            try
            {
                var isValid = Validate(item);

                if (isValid.Item1)
                {
                    switch (item.Length)
                    {
                        case 8:
                            {
                                Mask = "####-####";
                                break;
                            }
                        case 9:
                            {
                                Mask = "#####-####";
                                break;
                            }
                        case 10:
                            {
                                Mask = "(##) ####-####";
                                break;
                            }
                        case 11:
                            {
                                Mask = "(##) #####-####";
                                break;
                            }
                        case 12:
                            {
                                Mask = "## - (##) ####-####";
                                break;
                            }
                        case 13:
                            {
                                Mask = "## - (##) #####-####";
                                break;
                            }
                        default:
                            {
                                Mask = "";
                                break;
                            }
                    }

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

        public override Tuple<bool, string> Generate()
        {
            throw new System.NotImplementedException();
        }

        public override Tuple<bool, string> Validate(string item)
        {
            var message = item;
            var result = true;

            try
            {
                var itemWithoutMask = item.RemoveMask();
                var isValid = itemWithoutMask.Length >= 8 && itemWithoutMask.Length <= 13;

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