


function salvar() {

    Swal.fire({
        title: 'Deseja criar novo ponto?',
        icon: "warning",
        showDenyButton: true,
        confirmButtonText: 'Sim',
        denyButtonText: `Cancelar`,
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            Swal.fire('Saved!', '', 'success')
            setTimeout(enviarform, 1500)
        } else if (result.isDenied) {
            Swal.fire('Ponto nao salvo', '', 'info')
        }
    })
}
function enviarform() {
    $("#CreateForm").submit();
}

function salvar2(id) {
    var id2 = id;

    Swal.fire({
        title: 'Tem certeza que quer excluir este ponto?',
        icon: "warning",
        showDenyButton: true,
        confirmButtonText: 'Sim',
        denyButtonText: `Cancelar`,
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            Swal.fire('Excluido!', '', 'success')
            setTimeout(enviarform2(id2), 1500)
        } else if (result.isDenied) {
            Swal.fire('Ponto nao excluido', '', 'error')
        }
    })
}
function enviarform3(id) {
    console.log(id)
    document.getElementById("idponto").innerText = id
    console.log(document.getElementById("idponto"))
    $("#Delete").submit();
}

function enviarform2(id) {
    var dados = new FormData();
    dados.append('IDPonto',id)
    console.log(dados)
    $.ajax({
        url: 'https://localhost:7047/Administrador/AdminPontos/RegistrodePontos',
        method: 'POST',
        data: dados,
        processDat: false

    })
    $("#Delete").submit();

}


