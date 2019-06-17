$(function () {
	$("#Descricao").textareaCount({
        maxCharacterSize: 2000,
		warningNumber: 40,
        displayFormat : "#input/#max | #words palavras"
	});
})