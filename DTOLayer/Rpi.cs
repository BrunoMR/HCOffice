namespace DTOLayer
{
    using System;
    using ServiceStack.DataAnnotations;

    public class Rpi
    {
        [PrimaryKey]
        public int Numero { get; set; }
     
        public DateTime Data { get; set; }
    }
}
