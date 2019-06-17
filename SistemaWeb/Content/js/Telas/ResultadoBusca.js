$(".detailSelected").click(function () {
    detailSelected();
});

function detailSelected() {
    var processos = [];
    $("#results input:checked").each(function () {
        processos.push($(this).closest("tr").find("#processo_Numero").val());
    });

    window.open("Detalhes?" + serialize(processos, "codes"));

}

serialize = function(obj, name) {
    var str = [];
    for(var p in obj)
        if (obj.hasOwnProperty(p)) {
            str.push(name + "=" + encodeURIComponent(obj[p]));
        }
    return str.join("&");
};
var checkboxes = $('#results input[type="checkbox"]');

$("#chkCheckAll").on("ifChecked ifUnchecked", function (event) {
    if (event.type == "ifChecked") {
        checkboxes.iCheck("check");
    } else {
        checkboxes.iCheck("uncheck");
    }
});

function PrintElem(elem, title, css) {
    var tmpWindow = window.open('', 'PRINT', 'height=400,width=600');
    var tmpDoc = tmpWindow.document;

    title = title || document.title;
    css = css || "";

    this.setTitle = function (newTitle) {
        title = newTitle || document.title;
    };

    this.setCSS = function (newCSS) {
        css = newCSS || "";
    };

    this.basicHtml5 = function (innerHTML) {
        return '<!doctype html><html>' + (innerHTML || "") + '</html>';
    };

    this.htmlHead = function (innerHTML) {
        return '<head>' + (innerHTML || "") + '</head>';
    };

    this.htmlTitle = function (title) {
        return '<title>' + (title || "") + '</title>';
    };

    this.styleTag = function (innerHTML) {
        return '<style>' + (innerHTML || "") + '</style>';
    };

    this.htmlBody = function (innerHTML) {
        return '<body>' + (innerHTML || "") + '</body>';
    };

    this.build = function () {
        tmpDoc.write(
            this.basicHtml5(
                this.htmlHead(
                    this.htmlTitle(title) + this.styleTag(css)
                ) + this.htmlBody(
                    document.querySelector(elem).innerHTML
                )
            )
        );
        tmpDoc.close(); // necessary for IE >= 10
    };

    this.print = function () {
        tmpWindow.focus(); // necessary for IE >= 10*/
        tmpWindow.print();
        tmpWindow.close();
    };

    this.build();
    return this;
};

$("#printResults").click(function () {
    DOMPrinter = PrintElem("#tabela-resultados");
    DOMPrinter.print();
});