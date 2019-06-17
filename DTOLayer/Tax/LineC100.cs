using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.Tax
{
    public class LineC100
    {
        public string Register { get; set; }
        public int KindOperation { get; set; }
        public int Emission { get; set; }
        public string CodeParticipant { get; set; }
        public string CodeModel { get; set; }
        public int CodeSituation { get; set; }
        public string Serial { get; set; }
        public string NumberDocument { get; set; }
        public string NfeKey { get; set; }
        public string DateDocument { get; set; }
        public string DateInOrOut { get; set; }
        public Decimal TotalValue { get; set; }
        public string PaymantKind { get; set; }
        public Decimal DiscountValue { get; set; }
        public Decimal ReductionValue { get; set; }
        public Decimal ProductsValue { get; set; }
        public string Freight { get; set; }
        public Decimal FreightValue { get; set; }
        public Decimal InsuranceValue { get; set; }
        public Decimal ExpensesValue { get; set; }
        public Decimal IcmsBaseValue { get; set; }
        public Decimal IcmsValue { get; set; }
        public Decimal IcmsStBaseValue { get; set; }
        public Decimal IcmsStValue { get; set; }
        public Decimal IpiValue { get; set; }
        public Decimal PisValue { get; set; }
        public Decimal CofinsValue { get; set; }
        public Decimal PisStValue { get; set; }
        public Decimal CofinsStValue { get; set; }

        public List<LineC170> LineC170List { get; set; }
    }
}
