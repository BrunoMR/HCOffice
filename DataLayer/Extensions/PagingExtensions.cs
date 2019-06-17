using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Extensions
{
    using ServiceStack.OrmLite;

    // http://stackoverflow.com/a/35002756/1459499
    public static class PagingExtensions
    {
        public static SqlExpression<T> Page<T>(this SqlExpression<T> exp, int? page, int? pageSize)
        {
            if (!page.HasValue || !pageSize.HasValue)
            {
                return exp;
            }

            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), 
                    "Page must be a number greater than 0.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), 
                    "PageSize must be a number greater than 0.");
            }

            var skip = (page.Value - 1) * pageSize.Value;
            var take = pageSize.Value;

            return exp.Limit(skip, take);
        }

        // http://stackoverflow.com/a/3176628/508681
        public static int? LimitToRange(this int? value, int? inclusiveMinimum, int? inclusiveMaximum)
        {
            if (!value.HasValue) return null;
            if (inclusiveMinimum.HasValue && value < inclusiveMinimum) { return inclusiveMinimum; }
            if (inclusiveMaximum.HasValue && value > inclusiveMaximum) { return inclusiveMaximum; }
            return value;
        }
    }
}
