//$(function () {
//    $("#busca").addClass("active");
//});

//(function (factory) {
//    if (typeof define === "function" && define.amd) {

//        // AMD. Register as an anonymous module.
//        define(["../widgets/datepicker"], factory);
//    } else {

//        // Browser globals
//        factory(jQuery.datepicker);
//    }
//}(function (datepicker) {

//    datepicker.regional["pt-BR"] = {
//        closeText: "Fechar",
//        prevText: "&#x3C;Anterior",
//        nextText: "Próximo&#x3E;",
//        currentText: "Hoje",
//        monthNames: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
//        "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
//        monthNamesShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun",
//        "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
//        dayNames: [
//            "Domingo",
//            "Segunda-feira",
//            "Terça-feira",
//            "Quarta-feira",
//            "Quinta-feira",
//            "Sexta-feira",
//            "Sábado"
//        ],
//        dayNamesShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb"],
//        dayNamesMin: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb"],
//        weekHeader: "Sm",
//        dateFormat: "dd/mm/yy",
//        firstDay: 0,
//        isRTL: false,
//        showMonthAfterYear: false,
//        yearSuffix: ""
//    };
//    datepicker.setDefaults(datepicker.regional["pt-BR"]);

//    return datepicker.regional["pt-BR"];

//}));

//$(function () {
//    $("#dataDep-1").datepicker(
//        $.datepicker.regional["pt-BR"]);
//});

//$(function () {
//    $("#dataDep-2").datepicker(
//        $.datepicker.regional["pt-BR"]);
//});

//$(function () {
//    $("#dataConcess-1").datepicker(
//        $.datepicker.regional["pt-BR"]);
//});

//$(function () {
//    $("#dataConcess-2").datepicker(
//        $.datepicker.regional["pt-BR"]);
//});

$(function () {

    $("#DataDepositoStart").attr("data-date-format", "dd/mm/yyyy");
    $("#DataDepositoStart").attr("data-date-autoclose", "true");
    $("#DataDepositoEnd").attr("data-date-format", "dd/mm/yyyy");
    $("#DataDepositoEnd").attr("data-date-autoclose", "true");

    $("#DataDepositoStart").datepicker().on("changeDate", function (e) {
        $("#DataDepositoEnd").datepicker("setStartDate", e.date);
    });
    $("#DataDepositoEnd").datepicker().on("changeDate", function (e) {
        $("#DataDepositoStart").datepicker("setEndDate", e.date);
    });

    $("#DataConcessaoStart").attr("data-date-format", "dd/mm/yyyy");
    $("#DataConcessaoStart").attr("data-date-autoclose", "true");
    $("#DataConcessaoEnd").attr("data-date-format", "dd/mm/yyyy");
    $("#DataConcessaoEnd").attr("data-date-autoclose", "true");

    $("#DataConcessaoStart").datepicker().on("changeDate", function (e) {
        $("#DataConcessaoEnd").datepicker("setStartDate", e.date);
    });
    $("#DataConcessaoEnd").datepicker().on("changeDate", function (e) {
        $("#DataConcessaoStart").datepicker("setEndDate", e.date);
    });
});

$("#search-form input:text:visible").keyup(function () {
    process();
});

$("select:visible").on("change", function () {
    process();
});

$("#Numero").select2({
    tags: true,
    tokenSeparators: [",", " "],
    maximumSelectionLength: 30
});

function formatRepo(repo) {
    if (repo.loading) return repo.text;

    return repo ? repo.text : "";
};

function formatRepoSelection(repo) {
    return repo ? repo.text : "";
};

$("#Procurador").select2({
    ajax: {
        //url: "/SistemaWeb/Search/SearchProcurador",
        url: "/Search/SearchProcurador",
        type: "GET",
        width: "resolve",
        dataType: "json",
        delay: 250,
        data: function (params) {
            var request = {
                "match": {
                    "nome": params.term
                }
            };
            return { request: JSON.stringify(request), from: ((params.page || 1) - 1) * 10, size: 50 };
        },
        processResults: function (data, params) {
            // parse the results into the format expected by Select2
            // since we are using custom formatting functions we do not need to
            // alter the remote JSON data, except to indicate that infinite
            // scrolling can be used
            params.page = params.page || 1;

            var reformattedArray = data.Hits.map(function (obj) {
                var rObj = {};
                rObj.id = obj.Source.Nome;
                rObj.text = obj.Source.Nome;
                return rObj;
            });

            return {
                results: reformattedArray,
                pagination: {
                    more: (params.page * 50) < data.Total
                }
            };
        },
        cache: false
    },
    escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
    minimumInputLength: 1,
    maximumSelectionLength: 10,
    templateResult: formatRepo,
    templateSelection: formatRepoSelection
});

$("#Titular").select2({
    ajax: {
        //url: "/SistemaWeb/Search/SearchTitular",
        url: "/Search/SearchTitular",
        type: "GET",
        width: "resolve",
        dataType: "json",
        delay: 250,
        data: function (params) {
            var request = {
                "match": {
                    "nome": params.term
                }
            };
            return { request: JSON.stringify(request), from: ((params.page || 1) - 1) * 10, size: 50 };
        },
        processResults: function (data, params) {
            // parse the results into the format expected by Select2
            // since we are using custom formatting functions we do not need to
            // alter the remote JSON data, except to indicate that infinite
            // scrolling can be used
            params.page = params.page || 1;

            var reformattedArray = data.Hits.map(function (obj) {
                var rObj = {};
                rObj.id = obj.Source.Nome;
                rObj.text = obj.Source.Nome;
                return rObj;
            });

            return {
                results: reformattedArray,
                pagination: {
                    more: (params.page * 50) < data.Total
                }
            };
        },
        cache: false
    },
    escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
    minimumInputLength: 1,
    maximumSelectionLength: 10,
    templateResult: formatRepo,
    templateSelection: formatRepoSelection
});

$("#Classe").select2({
    ajax: {
        //url: "/SistemaWeb/Search/SearchClasse",
        url: "/Search/SearchClasse",
        type: "GET",
        width: "resolve",
        dataType: "json",
        delay: 250,
        data: function (params) {
            var request = {
                "query_string": {
                    "analyze_wildcard": true,
                    "default_field": "classe",
                    "query": "*" + params.term + "*"
                }
            };
            return { request: JSON.stringify(request), from: ((params.page || 1) - 1) * 10, size: 50 };
        },
        processResults: function (data, params) {
            // parse the results into the format expected by Select2
            // since we are using custom formatting functions we do not need to
            // alter the remote JSON data, except to indicate that infinite
            // scrolling can be used
            params.page = params.page || 1;

            var reformattedArray = data.Hits.map(function (obj) {
                var rObj = {};
                rObj.id = obj.Source.Classe;
                rObj.text = obj.Source.Classe;
                return rObj;
            });

            return {
                results: reformattedArray,
                pagination: {
                    more: (params.page * 50) < data.Total
                }
            };
        },
        cache: false
    },
    escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
    minimumInputLength: 1,
    maximumSelectionLength: 10,
    templateResult: formatRepo,
    templateSelection: formatRepoSelection
});

$("#Cfe4").select2({
    ajax: {
        //url: "/SistemaWeb/Search/SearchCfe4",
        url: "/Search/SearchCfe4",
        type: "GET",
        width: "resolve",
        dataType: "json",
        delay: 250,
        data: function (params) {
            var request = {
                "query_string": {
                    "analyze_wildcard": true,
                    "default_field": "codigo",
                    "query": "*" + params.term + "*"
                }
            };
            return { request: JSON.stringify(request), from: ((params.page || 1) - 1) * 10, size: 50 };
        },
        processResults: function (data, params) {
            // parse the results into the format expected by Select2
            // since we are using custom formatting functions we do not need to
            // alter the remote JSON data, except to indicate that infinite
            // scrolling can be used
            params.page = params.page || 1;

            var reformattedArray = data.Hits.map(function (obj) {
                var rObj = {};
                rObj.id = obj.Source.Codigo;
                rObj.text = obj.Source.Codigo;
                return rObj;
            });

            return {
                results: reformattedArray,
                pagination: {
                    more: (params.page * 50) < data.Total
                }
            };
        },
        cache: false
    },
    escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
    minimumInputLength: 1,
    maximumSelectionLength: 10,
    templateResult: formatRepo,
    templateSelection: formatRepoSelection
});

$("#Despacho").select2({
    ajax: {
        //url: "/SistemaWeb/Search/SearchDespacho",
        url: "/Search/SearchDespacho",
        type: "POST",
        width: "resolve",
        dataType: "json",
        delay: 250,
        data: function (params) {
            var request = {
                "query_string": {
                    "analyze_wildcard": true,
                    "default_field": "codigo",
                    "query": "*" + params.term + "*"
                }
            };
            return { request: JSON.stringify(request), from: ((params.page || 1) - 1) * 10, size: 50 };
        },
        processResults: function (data, params) {
            // parse the results into the format expected by Select2
            // since we are using custom formatting functions we do not need to
            // alter the remote JSON data, except to indicate that infinite
            // scrolling can be used
            params.page = params.page || 1;

            var reformattedArray = data.Hits.map(function (obj) {
                var rObj = {};
                rObj.id = obj.Source.Codigo;
                rObj.text = obj.Source.Codigo;
                return rObj;
            });

            return {
                results: reformattedArray,
                pagination: {
                    more: (params.page * 50) < data.Total
                }
            };
        },
        cache: false
    },
    escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
    minimumInputLength: 1,
    maximumSelectionLength: 10,
    templateResult: formatRepo,
    templateSelection: formatRepoSelection
});

$("#Apresentacao").select2({
    ajax: {
        //url: "/SistemaWeb/Search/SearchApresentacao",
        url: "/Search/SearchApresentacao",
        type: "POST",
        width: "resolve",
        dataType: "json",
        delay: 250,
        data: function (params) {
            var request = {
                "query_string": {
                    "analyze_wildcard": true,
                    "default_field": "apresentacaoDescricao",
                    "query": "*" + params.term + "*"
                }
            };
            return { request: JSON.stringify(request), from: ((params.page || 1) - 1) * 10, size: 50 };
        },
        processResults: function (data, params) {
            // parse the results into the format expected by Select2
            // since we are using custom formatting functions we do not need to
            // alter the remote JSON data, except to indicate that infinite
            // scrolling can be used
            params.page = params.page || 1;

            var reformattedArray = data.Hits.map(function (obj) {
                var rObj = {};
                rObj.id = obj.Source.ApresentacaoDescricao;
                rObj.text = obj.Source.ApresentacaoDescricao;
                return rObj;
            });

            return {
                results: reformattedArray,
                pagination: {
                    more: (params.page * 50) < data.Total
                }
            };
        },
        cache: false
    },
    escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
    minimumInputLength: 1,
    maximumSelectionLength: 10,
    templateResult: formatRepo,
    templateSelection: formatRepoSelection
});

$("#Pais").select2({
    ajax: {
        //url: "/SistemaWeb/Search/SearchPais",
        url: "/Search/SearchPais",
        type: "POST",
        width: "resolve",
        dataType: "json",
        delay: 250,
        data: function (params) {
            var request = {
                "query_string": {
                    "analyze_wildcard": true,
                    "default_field": "paisNome",
                    "query": "*" + params.term + "*"
                }
            };
            return { request: JSON.stringify(request), from: ((params.page || 1) - 1) * 10, size: 50 };
        },
        processResults: function (data, params) {
            // parse the results into the format expected by Select2
            // since we are using custom formatting functions we do not need to
            // alter the remote JSON data, except to indicate that infinite
            // scrolling can be used
            params.page = params.page || 1;

            var reformattedArray = data.Hits.map(function (obj) {
                var rObj = {};
                rObj.id = obj.Source.PaisNome;
                rObj.text = obj.Source.PaisNome;
                return rObj;
            });

            return {
                results: reformattedArray,
                pagination: {
                    more: (params.page * 50) < data.Total
                }
            };
        },
        cache: false
    },
    escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
    minimumInputLength: 1,
    maximumSelectionLength: 10,
    templateResult: formatRepo,
    templateSelection: formatRepoSelection
});

$("#Estado").select2({
    ajax: {
        //url: "/SistemaWeb/Search/SearchEstado",
        url: "/Search/SearchEstado",
        type: "POST",
        width: "resolve",
        dataType: "json",
        delay: 250,
        data: function (params) {
            var request = {
                "query_string": {
                    "analyze_wildcard": true,
                    "default_field": "estadoNome",
                    "query": "*" + params.term + "*"
                }
            };
            return { request: JSON.stringify(request), from: ((params.page || 1) - 1) * 10, size: 50 };
        },
        processResults: function (data, params) {
            // parse the results into the format expected by Select2
            // since we are using custom formatting functions we do not need to
            // alter the remote JSON data, except to indicate that infinite
            // scrolling can be used
            params.page = params.page || 1;

            var reformattedArray = data.Hits.map(function (obj) {
                var rObj = {};
                rObj.id = obj.Source.EstadoNome;
                rObj.text = obj.Source.EstadoNome;
                return rObj;
            });

            return {
                results: reformattedArray,
                pagination: {
                    more: (params.page * 50) < data.Total
                }
            };
        },
        cache: false
    },
    escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
    minimumInputLength: 1,
    maximumSelectionLength: 10,
    templateResult: formatRepo,
    templateSelection: formatRepoSelection
});

function process() {
    // get the first filled field id
    var firstFieldNonEmptyId = $("input:text:visible:filled:first").attr("id");

    // get field number from field id (Field 1 or Field 2 and so on)
    var firstFieldNonEmptyNumber = fields[firstFieldNonEmptyId];

    // if the user didn't select any filters 
    // then enable all fields
    if (!firstFieldNonEmptyNumber) {
        enableAllFields();
        return;
    }

    // get fields combination that 
    // can be filled along with the first filled field
    //var combinations = [];
    //combinations = combinationsList[firstFieldNonEmptyNumber];

    //// add the own filled field as a possible combination
    //combinations.push(firstFieldNonEmptyNumber);

    //// enable all fields that belong to the combination of fields
    //// that can be filled along with the first filled field
    //enablePossibleCombinations(combinations);
}

function enableAllFields() {
    $("#search-form :input")
        .removeAttr("disabled");
}

function enablePossibleCombinations(combinations) {
    var notDisableElements = getKeysFromValues(combinations);

    var selector = getMultipleFieldsSelector(notDisableElements);

    $("#search-form :input")
           .not(selector)
		   .not(".select2-search__field")
           .not(".advanced-filters select")
           .not("input[type='submit']")
           .prop("disabled", true);
}

var fields = {
    'Numero': 1,
    'Marca': 2,
    'Classe': 3,
    'Especificacao': 4,
    'Cfe4': 5,
    'CpfCnpjInpi': 6,
    'Titular': 7,
    'Procurador': 8,
    'Rpi': 9,
    'Despacho': 10,

    'DataDepositoStart': 11,
    'DataDepositoEnd': 20,
    'DataConcessaoStart': 21,
    'DataConcessaoEnd': 22,

    'Apresentacao': 12,

    'Phonetic': 13,
    'Affinity': 14,
    'Extinct': 15,
    'OnlyExtinct': 16,

    'Prefix': 17,
    'Suffix': 18,
    'Radical': 19,
    'ExactBrand': 23,
    'ExactWord': 24,
    'WithSpace': 25
};

var combinationsList = {
    1: [],
    2: [3, 4, 5, 7, 8, 9, 12, 13, 14, 15, 16, 17, 18, 19, 23, 24, 25],
    3: [2, 4, 5, 7, 8, 9, 10, 11, 20, 21, 22, 12, 14, 16, 17, 18, 19, 23, 24, 25],
    4: [2, 3, 5, 7, 8, 9, 10, 11, 20, 21, 22, 12, 15, 16, 17, 18, 19, 23, 24, 25],
    5: [2, 3, 4, 7, 8, 9, 10, 11, 20, 21, 22, 12, 15, 16, 17, 18, 19, 23, 24, 25],
    6: [],
    7: [2, 3, 4, 5, 8, 9, 10, 11, 20, 21, 22, 12, 15, 16, 17, 18, 19, 23, 24, 25],
    8: [2, 3, 4, 5, 7, 9, 10, 11, 20, 21, 22, 12, 15, 16, 17, 18, 19, 23, 24, 25],
    9: [2, 3, 4, 5, 7, 8, 10, 11, 20, 21, 22, 12, 15, 16, 17, 18, 19, 23, 24, 25],
    10: [2, 3, 4, 5, 7, 8, 9, 11, 20, 21, 22, 12, 15, 16, 17, 18, 19, 23, 24, 25],
    11: [2, 3, 4, 5, 7, 8, 9, 10, 12, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25],
    12: [2, 3, 4, 5, 7, 8, 9, 10, 11, 15, 16, 17, 18, 19, 23, 24, 25]
};

// helpers
function getKeysFromValues(values) {
    var keys = [];

    for (var i = 0; i < values.length; i++) {
        keys.push.apply(keys, getKeysFromValue(values[i]));
    }

    return keys;
}

function getKeysFromValue(value) {
    return Object.keys(fields)
        .filter(
        function (key) {
            return fields[key] === value;
        });
}

function getMultipleFieldsSelector(fields) {
    var selector = "";

    for (var i = 0; i < fields.length; i++) {
        selector = selector + " [name=" + fields[i] + "], ";
    }

    return removeTrailingCommas(selector.trim());
}

function removeTrailingCommas(selector) {
    return selector.replace(/(^,)|(,$)/g, "");
}