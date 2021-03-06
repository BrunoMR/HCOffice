﻿namespace BusinessLayer.Validation.ClasseNacional
{
    using DTOLayer;

    public class ClasseNacionalPropertyThird : IClasseNacionalProperty
    {
        public string IsValidProperty(ProcessoImported processo)
        {
            var codigo = ClasseNegocio.BuildClasseNacionalAndSubClasse(processo, 2);
            if (string.IsNullOrWhiteSpace(codigo))
                return string.Empty;

            var classeNegocio = new ClasseNegocio();

            return classeNegocio.ExistsClasse(codigo)
                ? string.Empty
                : string.Format("A classe '{0}' do Processo '{1}' não foi cadastrada!", codigo, processo.NumeroProcesso);
        }
    }
}