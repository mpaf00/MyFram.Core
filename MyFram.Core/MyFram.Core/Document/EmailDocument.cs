namespace MyFram.Core.Document
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Util;

    public class EmailDocument : BaseDocument
    {
        public override Tuple<bool, string> ApplyMask(string item)
        {
            return new Tuple<bool, string>(true, item);
        }

        public override Tuple<bool, string> Generate()
        {
            throw new NotImplementedException();
        }

        public override Tuple<bool, string> RemoveMask(string item)
        {
            return new Tuple<bool, string>(true, item);
        }

        public override Tuple<bool, string> Validate(string item)
        {
            var message = item;
            var result = true;

            try
            {
                var isValid = false;

                if (!String.IsNullOrEmpty(item))
                {
                    item = Regex.Replace(item, @"(@)(.+)$", DomainMapper, RegexOptions.None);

                    isValid = Regex.IsMatch(item,
                                             @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                             @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                                             RegexOptions.IgnoreCase);
                }

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

        private string DomainMapper(Match match)
        {
            var idn = new IdnMapping();

            var domainName = match.Groups[2].Value;

            domainName = idn.GetAscii(domainName);

            return match.Groups[1].Value + domainName;
        }
    }
}