


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