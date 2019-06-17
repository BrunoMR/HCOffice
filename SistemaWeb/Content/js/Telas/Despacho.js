$(function () {
	$("#Descricao").textareaCount({
			maxCharacterSize: 255,
			warningNumber: 40,
			displayFormat : "#input/#max | #words palavras"
		});
    $("#DescricaoIngles").textareaCount({
			maxCharacterSize: 255,
			warningNumber: 40,
			displayFormat : "#input/#max | #words words"
		});
    $("#DescricaoCompleta").textareaCount({
		maxCharacterSize: -2,
		warningNumber: 40,
        displayFormat : "#input | #words palavras"
	});
    $("#DescricaoCompletaIngles").textareaCount({
		maxCharacterSize: -2,
		warningNumber: 40,
        displayFormat : "#input | #words words"
	});
})