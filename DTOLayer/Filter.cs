namespace DTOLayer
{
    using System.Collections.Generic;
    using Enum;
    using System;
    using Nest;
    using System.Linq;

    public class FilterSearchModel
    {
        public string Field { get; set; }
        public List<Tuple<string, string>> Values { get; set; }
        public TypeSearch Type { get; set; }
        public Combination Combination { get; set; }
        public IList<ISort> SortList { get; set; }

        public FilterSearchModel()
        {

        }

        public FilterSearchModel(string field, IEnumerable<Tuple<string, string>> values, TypeSearch type, Combination combination = Combination.And)
        {
            Field = field;
            Type = type;
            Values = values.ToList();
            Combination = combination;
        }
    }
}
