


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
        if (result.isConfirmed) {
            Swal.fire({
                title: 'Excluido!',
                icon: "success",
                showDenyButton: true,
                confirmButtonText: 'Ok',
                denyButtonText: 'Desistir',
            }).then((result) => {
                if (result.isConfirmed) {
                    setTimeout(enviarform2(id2), 1500)
                }
            })
           
        } else if (result.isDenied) {
            Swal.fire('Ponto nao excluido', '', 'error')
        }
    })
}
function enviarform2(id) {
    console.log(id)
    var dados = new FormData();
    document.getElementById("inputid").value = id;
    dados.append('IDPonto', id)
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("POST", "https://localhost:7047/Administrador/AdminPontos/RegistrodePontos", true)
    xmlhttp.send(dados);
    $("#Delete").submit()


}


