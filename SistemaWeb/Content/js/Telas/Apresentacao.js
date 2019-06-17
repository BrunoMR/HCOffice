$(function () {
    $("#Descricao").textareaCount({
        maxCharacterSize: 100,
		warningNumber: 40,
        displayFormat : "#input/#max | #words palavras"
	});
    $("#DescricaoIngles").textareaCount({
		maxCharacterSize: 100,
		warningNumber: 40,
        displayFormat : "#input/#max | #words words"
	});
})