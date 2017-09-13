using ServiceStack.OrmLite.Converters;

namespace ServiceStack.OrmLite.MySqlConnector.Converters
{
    public class MySqlStringConverter : StringConverter
    {
        public MySqlStringConverter() : base(255) {}

        public override string MaxColumnDefinition => "LONGTEXT";
    }

    public class MySqlCharArrayConverter : CharArrayConverter
    {
        public MySqlCharArrayConverter() : base(255) { }

        public override string MaxColumnDefinition => "LONGTEXT";
    }
}