using System;
using System.Collections.Generic;
using System.Linq;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.Core.Framework.Mvc.Models.JqGrid
{
    public class JqSearchIn
    {
        public string Sidx { get; set; }
        public string Sord { get; set; }
        public int Page { get; set; }
        public int Rows { get; set; }
        public bool _Search { get; set; }
        public string SearchField { get; set; }
        public string SearchOper { get; set; }
        public string SearchString { get; set; }
        public string Filters { get; set; }

        public WhereClause GenerateWhereClause<T>()
        {
            return new WhereClauseGenerator().Generate(_Search, Filters, typeof(T));
        }

        public JqGridSearchOut Search<T>(IQueryable<T> data)
        {
            int totalRecords;
            var skip = (Page > 0 ? Page - 1 : 0) * Rows;

            if (string.IsNullOrEmpty(Sidx))
            {
                var defaultOrderProperty =
                    (typeof(T)).GetProperties().FirstOrDefault(
                        pi => pi.GetCustomAttributes(typeof(DefaultOrderAttribute), false).Length > 0);
                if (defaultOrderProperty != null)
                    Sidx = defaultOrderProperty.Name;
                else
                {
                    var firstProperty = (typeof(T)).GetProperties().FirstOrDefault();
                    if (firstProperty != null) Sidx = firstProperty.Name;
                    else throw new Exception("Object have no property.");
                }
            }
            var order = string.Format("{0} {1}", Sidx, Sord);

            if (_Search && !string.IsNullOrEmpty(Filters))
            {
                var wc = GenerateWhereClause<T>();

                data = data.Where(wc.Clause, wc.FormatObjects);

                totalRecords = data.Count();

                data = data
                    .OrderBy(order)
                    .Skip(skip)
                    .Take(Rows);
            }
            else
            {
                totalRecords = data.Count();
                data = data.OrderBy(order)
                    .Skip(skip)
                    .Take(Rows);
            }
            var totalPages = (int)Math.Ceiling((float)totalRecords / Rows);

            var grid = new JqGridSearchOut
            {
                total = totalPages,
                page = Page,
                records = totalRecords,
                rows = data.ToArray()
            };
            return grid;
        }
    }

    public class JqGridSearchOut
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public Array rows { get; set; }
    }

    public class JqGridFilter
    {
        public GroupOp GroupOp { get; set; }
        public List<JqGridRule> Rules { get; set; }
        public List<JqGridFilter> Groups { get; set; }
    }

    public class JqGridRule
    {
        public string Field { get; set; }
        public Operations Op { get; set; }
        public string Data { get; set; }
    }

    public class WhereClause
    {
        public string Clause { get; set; }
        public object[] FormatObjects { get; set; }
    }
}
