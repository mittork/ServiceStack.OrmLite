using System;
using System.Configuration;
using System.Data;
using NUnit.Framework;
using ServiceStack.Logging;
using ServiceStack.OrmLite.MySqlConnector;

namespace ServiceStack.OrmLite.MySqlConnector.Tests
{
	public class OrmLiteTestBase
	{
		protected virtual string ConnectionString { get; set; }

		[OneTimeSetUp]
		public void TestFixtureSetUp()
		{
			LogManager.LogFactory = new ConsoleLogFactory();

		    OrmLiteConfig.DialectProvider = MySqlConnectorDialectProvider.Instance;
		    ConnectionString = ConfigurationManager.ConnectionStrings["testDb"].ConnectionString;
		}

		public void Log(string text)
		{
			Console.WriteLine(text);
		}

        public virtual IDbConnection OpenDbConnection(string connString = null)
        {
            connString = connString ?? ConnectionString;
            return connString.OpenDbConnection();
        }
    }
}