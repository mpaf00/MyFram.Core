namespace MyFram.Core.Document
{
    using System;
    using System.Globalization;
    using Util;

    public class CnpjDocument : BaseDocument
    {
        public CnpjDocument()
        {
            Mask = "##.###.###/####-##";
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
                const int n9 = 0;
                const int n10 = 0;
                const int n11 = 0;
                const int n12 = 1;

                var aux = n1 * 5 + n2 * 4 + n3 * 3 + n4 * 2 + n5 * 9 + n6 * 8 + n7 * 7 + n8 * 6 + n9 * 5 + n10 * 4 + n11 * 3 + n12 * 2;
                aux = aux % 11;
                var nv1 = aux < 2 ? 0 : 11 - aux;

                aux = n1 * 6 + n2 * 5 + n3 * 4 + n4 * 3 + n5 * 2 + n6 * 9 + n7 * 8 + n8 * 7 + n9 * 6 + n10 * 5 + n11 * 4 + n12 * 3 + nv1 * 2;
                aux = aux % 11;
                var nv2 = aux < 2 ? 0 : 11 - aux;

                var item = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}", n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, nv1, nv2);
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

                var multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                var itemWithoutMask = item.RemoveMask();

                if (itemWithoutMask.Length == 14)
                {
                    var tempCnpj = itemWithoutMask.Substring(0, 12);
                    var soma = 0;

                    for (var i = 0; i < 12; i++)
                    {
                        soma += int.Parse(tempCnpj[i].ToString(CultureInfo.InvariantCulture)) * multiplicador1[i];
                    }

                    var resto = (soma % 11);
                    resto = resto < 2 ? 0 : 11 - resto;

                    var digito = resto.ToString(CultureInfo.InvariantCulture);
                    tempCnpj = tempCnpj + digito;
                    soma = 0;

                    for (var i = 0; i < 13; i++)
                    {
                        soma += int.Parse(tempCnpj[i].ToString(CultureInfo.InvariantCulture)) * multiplicador2[i];
                    }

                    resto = (soma % 11);
                    resto = resto < 2 ? 0 : 11 - resto;
                    digito += resto.ToString(CultureInfo.InvariantCulture);

                    isValid = itemWithoutMask.EndsWith(digito);
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