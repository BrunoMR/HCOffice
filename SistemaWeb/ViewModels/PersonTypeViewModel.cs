using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaWeb.ViewModels
{
    public class PersonTypeViewModel
    {
        public char Tipo { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public bool IsNew { get; set; }
    }
}