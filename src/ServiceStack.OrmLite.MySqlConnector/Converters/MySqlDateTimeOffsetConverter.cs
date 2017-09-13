using ServiceStack.OrmLite.Converters;

namespace ServiceStack.OrmLite.MySqlConnector.Converters
{
    public class MySqlDateTimeOffsetConverter : DateTimeOffsetConverter
    {
        public override string ColumnDefinition => "VARCHAR(255)";
    }
}