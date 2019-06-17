namespace DTOLayer.Tax
{
    using System.Collections.Generic;

    public class SPED // FileEfd
    {
        public List<LineC100> LineC100List { get; set; }
        public List<Line0150> Line0150List { get; set; }
        public List<Line0400> Line0400List { get; set; }
    }
    
}
