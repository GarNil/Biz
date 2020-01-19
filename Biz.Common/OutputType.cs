namespace Biz.Common
{
    public enum OutputType
    {
        PlainText = 1,
        Json = 2,
        Xml = 3
    }

    public static class OutputTypeExtension
    {
        public static IFormatterEnumerable<OutputRowModel> GetFormatter(this OutputType type)
        {
            switch (type)
            {
                case OutputType.Json:
                    return new JsonFormatterEnumerableOutputRowModel();
                case OutputType.Xml:
                    return new XmlFormatterEnumerableOutputRowModel();
                case OutputType.PlainText:
                    return new PlainTextFormatterEnumerableOutputRowModel();
            }
            return null;
        }
    }
}
