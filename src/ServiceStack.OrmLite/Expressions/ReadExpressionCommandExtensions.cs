using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using ServiceStack.Text;

namespace ServiceStack.OrmLite
{
    internal static class ReadExpressionCommandExtensions
    {
        internal static List<T> Select<T>(this IDbCommand dbCmd, SqlExpression<T> q)
        {
            string sql = q.SelectInto<T>();
            return dbCmd.ExprConvertToList<T>(sql, q.Params, onlyFields: q.OnlyFields);
        }

        internal static List<T> Select<T>(this IDbCommand dbCmd, Expression<Func<T, bool>> predicate)
        {
            var q = dbCmd.GetDialectProvider().SqlExpression<T>();
            string sql = q.Where(predicate).SelectInto<T>();

            return dbCmd.ExprConvertToList<T>(sql, q.Params);
        }

        internal static List<Tuple<T, T2>> SelectMulti<T, T2>(this IDbCommand dbCmd, SqlExpression<T> q)
        {
            q.Select(q.CreateMultiSelect<T, T2, EOT, EOT, EOT, EOT, EOT>(dbCmd.GetDialectProvider()));
            return dbCmd.ExprConvertToList<Tuple<T, T2>>(q.ToSelectStatement(), q.Params, onlyFields: q.OnlyFields);
        }

        internal static List<Tuple<T, T2, T3>> SelectMulti<T, T2, T3>(this IDbCommand dbCmd, SqlExpression<T> q)
        {
            q.Select(q.CreateMultiSelect<T, T2, T3, EOT, EOT, EOT, EOT>(dbCmd.GetDialectProvider()));
            return dbCmd.ExprConvertToList<Tuple<T, T2, T3>>(q.ToSelectStatement(), q.Params, onlyFields: q.OnlyFields);
        }

        internal static List<Tuple<T, T2, T3, T4>> SelectMulti<T, T2, T3, T4>(this IDbCommand dbCmd, SqlExpression<T> q)
        {
            q.Select(q.CreateMultiSelect<T, T2, T3, T4, EOT, EOT, EOT>(dbCmd.GetDialectProvider()));
            return dbCmd.ExprConvertToList<Tuple<T, T2, T3, T4>>(q.ToSelectStatement(), q.Params, onlyFields: q.OnlyFields);
        }

        internal static List<Tuple<T, T2, T3, T4, T5>> SelectMulti<T, T2, T3, T4, T5>(this IDbCommand dbCmd, SqlExpression<T> q)
        {
            q.Select(q.CreateMultiSelect<T, T2, T3, T4, T5, EOT, EOT>(dbCmd.GetDialectProvider()));
            return dbCmd.ExprConvertToList<Tuple<T, T2, T3, T4, T5>>(q.ToSelectStatement(), q.Params, onlyFields: q.OnlyFields);
        }

        internal static List<Tuple<T, T2, T3, T4, T5, T6>> SelectMulti<T, T2, T3, T4, T5, T6>(this IDbCommand dbCmd, SqlExpression<T> q)
        {
            q.Select(q.CreateMultiSelect<T, T2, T3, T4, T5, T6, EOT>(dbCmd.GetDialectProvider()));
            return dbCmd.ExprConvertToList<Tuple<T, T2, T3, T4, T5, T6>>(q.ToSelectStatement(), q.Params, onlyFields: q.OnlyFields);
        }

        internal static List<Tuple<T, T2, T3, T4, T5, T6, T7>> SelectMulti<T, T2, T3, T4, T5, T6, T7>(this IDbCommand dbCmd, SqlExpression<T> q)
        {
            q.Select(q.CreateMultiSelect<T, T2, T3, T4, T5, T6, T7>(dbCmd.GetDialectProvider()));
            return dbCmd.ExprConvertToList<Tuple<T, T2, T3, T4, T5, T6, T7>>(q.ToSelectStatement(), q.Params, onlyFields: q.OnlyFields);
        }

        internal static string CreateMultiSelect<T, T2, T3, T4, T5, T6, T7>(this SqlExpression<T> q, IOrmLiteDialectProvider dialectProvider)
        {
            var sb = StringBuilderCache.Allocate()
                .AppendFormat("{0}.*, 0 EOT", dialectProvider.GetQuotedTableName(typeof(T).GetModelDefinition()));

            if (typeof(T2) != typeof(EOT))
                sb.Append($", {dialectProvider.GetQuotedTableName(typeof(T2).GetModelDefinition())}.*, 0 EOT");
            if (typeof(T3) != typeof(EOT))
                sb.Append($", {dialectProvider.GetQuotedTableName(typeof(T3).GetModelDefinition())}.*, 0 EOT");
            if (typeof(T4) != typeof(EOT))
                sb.Append($", {dialectProvider.GetQuotedTableName(typeof(T4).GetModelDefinition())}.*, 0 EOT");
            if (typeof(T5) != typeof(EOT))
                sb.Append($", {dialectProvider.GetQuotedTableName(typeof(T5).GetModelDefinition())}.*, 0 EOT");
            if (typeof(T6) != typeof(EOT))
                sb.Append($", {dialectProvider.GetQuotedTableName(typeof(T6).GetModelDefinition())}.*, 0 EOT");
            if (typeof(T7) != typeof(EOT))
                sb.Append($", {dialectProvider.GetQuotedTableName(typeof(T7).GetModelDefinition())}.*, 0 EOT");

            return StringBuilderCache.ReturnAndFree(sb);
        }

        internal static T Single<T>(this IDbCommand dbCmd, Expression<Func<T, bool>> predicate)
        {
            var q = dbCmd.GetDialectProvider().SqlExpression<T>();

            return Single(dbCmd, q.Where(predicate));
        }

        internal static T Single<T>(this IDbCommand dbCmd, SqlExpression<T> q)
        {
            string sql = q.Limit(1).SelectInto<T>();

            return dbCmd.ExprConvertTo<T>(sql, q.Params, onlyFields:q.OnlyFields);
        }

        public static TKey Scalar<T, TKey>(this IDbCommand dbCmd, SqlExpression<T> expression)
        {
            var sql = expression.SelectInto<T>();
            return dbCmd.Scalar<TKey>(sql, expression.Params);
        }

        public static TKey Scalar<T, TKey>(this IDbCommand dbCmd, Expression<Func<T, object>> field)
        {
            var q = dbCmd.GetDialectProvider().SqlExpression<T>();
            q.Select(field);
            var sql = q.SelectInto<T>();
            return dbCmd.Scalar<TKey>(sql, q.Params);
        }

        internal static TKey Scalar<T, TKey>(this IDbCommand dbCmd,
            Expression<Func<T, object>> field, Expression<Func<T, bool>> predicate)
        {
            var q = dbCmd.GetDialectProvider().SqlExpression<T>();
            q.Select(field).Where(predicate);
            string sql = q.SelectInto<T>();
            return dbCmd.Scalar<TKey>(sql, q.Params);
        }

        internal static long Count<T>(this IDbCommand dbCmd)
        {
            var q = dbCmd.GetDialectProvider().SqlExpression<T>();
            var sql = q.ToCountStatement();
            return GetCount(dbCmd, sql, q.Params);
        }

        internal static long Count<T>(this IDbCommand dbCmd, SqlExpression<T> expression)
        {
            var sql = expression.ToCountStatement();
            return GetCount(dbCmd, sql, expression.Params);
        }

        internal static long Count<T>(this IDbCommand dbCmd, Expression<Func<T, bool>> predicate)
        {
            var q = dbCmd.GetDialectProvider().SqlExpression<T>();
            q.Where(predicate);
            var sql = q.ToCountStatement();
            return GetCount(dbCmd, sql, q.Params);
        }

        internal static long GetCount(this IDbCommand dbCmd, string sql)
        {
            return dbCmd.Column<long>(sql).Sum();
        }

        internal static long GetCount(this IDbCommand dbCmd, string sql, IEnumerable<IDbDataParameter> sqlParams)
        {
            return dbCmd.Column<long>(sql, sqlParams).Sum();
        }

        internal static long RowCount<T>(this IDbCommand dbCmd, SqlExpression<T> expression)
        {
            //ORDER BY throws when used in subselects in SQL Server. Removing OrderBy() clause since it doesn't impact results
            var countExpr = expression.Clone().OrderBy(); 
            return dbCmd.Scalar<long>(dbCmd.GetDialectProvider().ToRowCountStatement(countExpr.ToSelectStatement()), expression.Params);
        }

        internal static long RowCount(this IDbCommand dbCmd, string sql, object anonType)
        {
            if (anonType != null)
                dbCmd.SetParameters(anonType.ToObjectDictionary(), excludeDefaults: false);

            return dbCmd.Scalar<long>(dbCmd.GetDialectProvider().ToRowCountStatement(sql));
        }

        internal static long RowCount(this IDbCommand dbCmd, string sql, IEnumerable<IDbDataParameter> sqlParams)
        {
            return dbCmd.SetParameters(sqlParams).Scalar<long>(dbCmd.GetDialectProvider().ToRowCountStatement(sql));
        }

        internal static List<T> LoadSelect<T>(this IDbCommand dbCmd, SqlExpression<T> expression = null, IEnumerable<string> include = null)
        {
            return dbCmd.LoadListWithReferences<T, T>(expression, include);
        }

        internal static List<Into> LoadSelect<Into, From>(this IDbCommand dbCmd, SqlExpression<From> expression, IEnumerable<string> include = null)
        {
            return dbCmd.LoadListWithReferences<Into, From>(expression, include);
        }

        internal static List<T> LoadSelect<T>(this IDbCommand dbCmd, Expression<Func<T, bool>> predicate, IEnumerable<string> include = null)
        {
            var expr = dbCmd.GetDialectProvider().SqlExpression<T>().Where(predicate);
            return dbCmd.LoadListWithReferences<T, T>(expr, include);
        }
    }
}

