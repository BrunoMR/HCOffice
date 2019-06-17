using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.Tax
{
    public class LineC170
    {
        public string Register { get; set; }
        public int NumberItem { get; set; }
        public string CodeItem { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public Decimal TotalValue { get; set; }
        public Decimal DiscountValue { get; set; }
        public string Move { get; set; }
        public int CstIcms { get; set; }
        public int Cfop { get; set; }
        public string CodeKind { get; set; }
        public Decimal IcmsBaseValue { get; set; }
        public Decimal IcmsPart { get; set; }
        public Decimal IcmsValue { get; set; }
        public Decimal IcmsStBaseValue { get; set; }
        public Decimal StPart { get; set; }
        public Decimal IcmsStValue { get; set; }
        public int BillingIndicator { get; set; }
        public string CstIpi { get; set; }
        public string CodeEnq { get; set; }
        public Decimal IpiBaseValue { get; set; }
        public Decimal IpiPart { get; set; }
        public Decimal IpiValue { get; set; }
        public string CstPis { get; set; }
        public Decimal PisBaseValue { get; set; }
        public Decimal PisPartPercent { get; set; }
        public Decimal PisAmountBase { get; set; }
        public Decimal PisPartValue { get; set; }
        public Decimal PisValue { get; set; }
        public string CstCofins { get; set; }
        public Decimal CofinsBaseValue { get; set; }
        public Decimal CofinsPartPercent { get; set; }
        public Decimal CofinsAmountBase { get; set; }
        public Decimal CofinsPartValue { get; set; }
        public Decimal CofinsValue { get; set; }
        public String CodeCta { get; set; }
    }
}
