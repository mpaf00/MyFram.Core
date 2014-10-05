namespace MyFram.Core.Document
{
    using System;
    using System.Globalization;
    using Util;

    public class CpfDocument : BaseDocument
    {
        public CpfDocument()
        {
            Mask = "###.###.###-##";
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

                var aux = n1 * 10 + n2 * 9 + n3 * 8 + n4 * 7 + n5 * 6 + n6 * 5 + n7 * 4 + n8 * 3 + n9 * 2;
                aux = aux % 11;
                var nv1 = aux < 2 ? 0 : 11 - aux;

                aux = n1 * 11 + n2 * 10 + n3 * 9 + n4 * 8 + n5 * 7 + n6 * 6 + n7 * 5 + n8 * 4 + n9 * 3 + nv1 * 2;
                aux = aux % 11;
                var nv2 = aux < 2 ? 0 : 11 - aux;

                var item = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}", n1, n2, n3, n4, n5, n6, n7, n8, n9, nv1, nv2);
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

                var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                var itemWithoutMask = item.RemoveMask();

                if (itemWithoutMask.Length == 11)
                {
                    var tempCpf = itemWithoutMask.Substring(0, 9);
                    var soma = 0;

                    for (var i = 0; i < 9; i++)
                    {
                        soma += int.Parse(tempCpf[i].ToString(CultureInfo.InvariantCulture)) * multiplicador1[i];
                    }

                    var resto = soma % 11;
                    resto = resto < 2 ? 0 : 11 - resto;

                    var digito = resto.ToString(CultureInfo.InvariantCulture);
                    tempCpf = tempCpf + digito;
                    soma = 0;

                    for (var i = 0; i < 10; i++)
                    {
                        soma += int.Parse(tempCpf[i].ToString(CultureInfo.InvariantCulture)) * multiplicador2[i];
                    }

                    resto = soma % 11;
                    resto = resto < 2 ? 0 : 11 - resto;
                    digito = digito + resto.ToString(CultureInfo.InvariantCulture);

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