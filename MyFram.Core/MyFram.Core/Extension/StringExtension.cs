namespace MyFram.Core.Extension
{
    using Pechkin;
    using Pechkin.Synchronized;

    public static class StringExtension
    {
        public static byte[] HtmlToPdf(this string item)
        {
            var config = new ObjectConfig();
            config.SetPrintBackground(true);
            return new SynchronizedPechkin(new GlobalConfig()).Convert(config, item);
        }
    }
}