namespace SistemaWeb
{
    using System.Web.Optimization;
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/dataTable").Include(
                "~/Content/js/plugins/datatables/jquery.dataTables.min.js",
                "~/Content/js/plugins/datatables/DT_bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/textArea").Include(
                "~/Content/js/plugins/textarea-counter/jquery.textarea-counter.js"));

             bundles.Add(new ScriptBundle("~/bundles/apresentacao").Include(
                "~/Content/js/Telas/Apresentacao.js"));

            bundles.Add(new ScriptBundle("~/bundles/cfe4").Include(
                "~/Content/js/Telas/Cfe4.js"));

            bundles.Add(new ScriptBundle("~/bundles/classe").Include(
                "~/Content/js/Telas/Classe.js"));

            bundles.Add(new ScriptBundle("~/bundles/despacho").Include(
                "~/Content/js/Telas/Despacho.js"));

            bundles.Add(new ScriptBundle("~/bundles/natureza").Include(
                "~/Content/js/Telas/Natureza.js"));

            bundles.Add(new ScriptBundle("~/bundles/atributo").Include(
                "~/Content/js/Telas/Atributo.js"));

            bundles.Add(new ScriptBundle("~/bundles/pais").Include(
                "~/Content/js/Telas/Pais.js"));

            bundles.Add(new ScriptBundle("~/bundles/estado").Include(
                "~/Content/js/Telas/Estado.js"));

            bundles.Add(new ScriptBundle("~/bundles/cadastros").Include(
                "~/Content/js/Telas/Cadastros.js"));

            bundles.Add(new ScriptBundle("~/bundles/busca").Include(
                "~/Content/js/Telas/Busca.js"));

            bundles.Add(new ScriptBundle("~/bundles/resultadoBusca").Include(
                "~/Content/js/Telas/ResultadoBusca.js",
                "~/Content/js/plugins/magnific/jquery.magnific-popup.js"));

            bundles.Add(new StyleBundle("~/Content/resultadoBusca").Include(
                "~/Content/css/Telas/ResultadoBusca.css",
                "~/Content/js/plugins/magnific/magnific-popup.css"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                "~/Scripts/toastr.js"));
            
            bundles.Add(new StyleBundle("~/Content/toastr").Include(
                "~/Content/toastr.min.css"));

            bundles.Add(new StyleBundle("~/Content/login").Include(
                "~/Content/login.css"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                      "~/Content/js/libs/bootstrap.js",
                      "~/Content/js/libs/jquery-1.9.1.min.js",
                      "~/Content/js/libs/jquery-ui-1.9.2.custom.min.js",
                      "~/Scripts/jquery.validate.js",
                      "~/Content/js/plugins/icheck/jquery.icheck.js",
                      "~/Content/js/App.js",
                      "~/Content/js/plugins/select2/select2.js",
                      "~/Content/js/plugins/timepicker/bootstrap-timepicker.js",
                      "~/Content/js/plugins/datepicker/bootstrap-datepicker.min.js",
                      "~/Content/js/plugins/datepicker/bootstrap-datepicker.pt-BR.js",
                      "~/Content/js/plugins/howl/howl.js"));

            bundles.Add(new StyleBundle("~/Content/cssBundle").Include(
                      "~/Content/Css/bootstrap.min.css",
                      "~/Content/Css/font-awesome.min.css",
                      "~/Content/Css/App.css",
                      "~/Content/Css/custom.css",
                      "~/Content/js/plugins/icheck/skins/all.css",
                      "~/Content/js/plugins/select2/select2.css",
                      "~/Content/js/plugins/timepicker/bootstrap-timepicker.css",
                      "~/Content/js/plugins/datepicker/datepicker.css",
                      "~/Content/js/libs/css/ui-lightness/jquery-ui-1.9.2.custom.min.css"));

            bundles.Add(new StyleBundle("~/Content/cssTemporary").Include(
                      "~/Content/Sass/Dist/bootstrap.min.css",
                      "~/Content/Css/font-awesome.min.css",
                      "~/Content/Sass/Dist/App.css",
                      "~/Content/js/plugins/icheck/skins/all.css",
                      "~/Content/js/plugins/select2/select2.css",
                      "~/Content/js/plugins/timepicker/bootstrap-timepicker.css",
                      "~/Content/js/plugins/datepicker/datepicker.css",
                      "~/Content/js/libs/css/ui-lightness/jquery-ui-1.9.2.custom.min.css"));
        }
    }
}
