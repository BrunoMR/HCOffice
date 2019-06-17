function MessageSucess() {
    alert("Backup criado com sucesso!");
};

function MessageError() {
    toastr["error"]("Não foi possível criar o Backup!", "Database");
};