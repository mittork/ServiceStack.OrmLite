using ServiceStack.OrmLite.Converters;

namespace ServiceStack.OrmLite.MySqlConnector.Converters
{
    public class MySqlDecimalConverter : DecimalConverter
    {
        public MySqlDecimalConverter() : base(38,6) { }
    }
}