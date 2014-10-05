namespace MyFram.Core.Util
{
    using System;
    using System.Linq;

    public static class Mask
    {
        public static string ApplyMask(this string item, string mask)
        {
            if (string.IsNullOrEmpty(mask))
            {
                throw new Exception(Message.InvalidMask);
            }

            var itemWithoutMask = item.RemoveMask();
            var itemWithMask = string.Empty;
            var k = 0;

            for (var i = 0; i <= mask.Length - 1; i++)
            {
                if (mask[i] == '#')
                {
                    itemWithMask += itemWithoutMask[k];
                    k++;
                }
                else
                {
                    itemWithMask += mask[i];
                }
            }

            return itemWithMask;
        }

        public static string RemoveMask(this string item)
        {
            var itemWithoutMask = new string(item.ToList().Where(char.IsLetterOrDigit).ToArray());

            return itemWithoutMask;
        }
    }
}