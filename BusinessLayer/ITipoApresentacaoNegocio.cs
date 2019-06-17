namespace BusinessLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface ITipoApresentacaoNegocio
    {
        #region CRUD

        List<TipoApresentacao> GetAll();
        TipoApresentacao FindById(int id);
        TipoApresentacao Save(TipoApresentacao tipoApresentacao);

        #endregion CRUD

        bool ExistsTipoApresentacao(string apresentacao);
    }
}
