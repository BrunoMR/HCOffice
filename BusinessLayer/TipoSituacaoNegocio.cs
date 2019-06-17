namespace BusinessLayer
{
    using System.Collections.Generic;
    using DataLayer;
    using DTOLayer;

    public class TipoSituacaoNegocio : ITipoSituacaoNegocio
    {
        readonly ITipoSituacaoRepository _tipoSituacaoRepository = new TipoSituacaoRepository();

        public List<TipoSituacao> GetAll()
        {
            return _tipoSituacaoRepository.GetAll();
        }

        public TipoSituacao FindById(int id)
        {
            return _tipoSituacaoRepository.FindById(id);
        }

        public TipoSituacao AddOrUpdate(TipoSituacao tipoSituacao)
        {
            return tipoSituacao.Tipo == null
                ? _tipoSituacaoRepository.Add(tipoSituacao)
                : _tipoSituacaoRepository.Update(tipoSituacao);
        }

        public void Remove(int id)
        {
            _tipoSituacaoRepository.Remove(id);
        }
    }
}
