using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOLayer;
using ExtratorDeDados.Repository;

namespace ExtratorDeDados.Negocio
{
    public class TipoSituacaoNegocio
    {
        public List<TipoSituacao> Buscar(TipoSituacao model)
        {
            ARepositorySelect<TipoSituacao> aRepositorySelect = new TipoSituacaoRepository();

            return aRepositorySelect.Buscar(model);
        }
    }
}
