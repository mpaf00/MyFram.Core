namespace MyFram.Core.Document
{
    using System;
    using System.Globalization;
    using Util;

    public class PisDocument : BaseDocument
    {
        public PisDocument()
        {
            Mask = "###.#####.##-#";
        }

        public override Tuple<bool, string> Generate()
        {
            string message;
            var result = true;

            try
            {
                var random = new Random();

                var n1 = random.Next(10);
                var n2 = random.Next(10);
                var n3 = random.Next(10);
                var n4 = random.Next(10);
                var n5 = random.Next(10);
                var n6 = random.Next(10);
                var n7 = random.Next(10);
                var n8 = random.Next(10);
                var n9 = random.Next(10);
                var n10 = random.Next(10);

                var aux = n1 * 3 + n2 * 2 + n3 * 9 + n4 * 8 + n5 * 7 + n6 * 6 + n7 * 5 + n8 * 4 + n9 * 3 + n10 * 2;
                aux = aux % 11;
                var nv1 = aux < 2 ? 0 : 11 - aux;

                var item = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}", n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, nv1);
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
                var isValid = false;

                var itemWithoutMask = item.RemoveMask();
                var multiplicador = new[] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                if (itemWithoutMask.Length == 11)
                {
                    var soma = 0;
                    for (var i = 0; i < 10; i++)
                    {
                        soma += int.Parse(itemWithoutMask[i].ToString(CultureInfo.InvariantCulture)) * multiplicador[i];
                    }

                    var resto = soma % 11;
                    resto = resto < 2 ? 0 : 11 - resto;

                    isValid = itemWithoutMask.EndsWith(resto.ToString(CultureInfo.InvariantCulture));
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
    }
}