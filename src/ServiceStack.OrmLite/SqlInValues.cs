using System.Collections;

namespace ServiceStack.OrmLite
{
    public class SqlInValues
    {
        private readonly IEnumerable values;
        private readonly IOrmLiteDialectProvider dialectProvider;

        public int Count { get; }

        public SqlInValues(IEnumerable values, IOrmLiteDialectProvider dialectProvider=null)
        {
            this.values = values;
            this.dialectProvider = dialectProvider ?? OrmLiteConfig.DialectProvider;

            if (values == null) return;
            foreach (var value in values)
            {
                ++Count;
            }
        }

        public string ToSqlInString()
        {
            return Count == 0 
                ? "NULL" 
                : OrmLiteUtils.SqlJoin(values, dialectProvider);
        }

        public IEnumerable GetValues()
        {
            return values;
        }
    }
}