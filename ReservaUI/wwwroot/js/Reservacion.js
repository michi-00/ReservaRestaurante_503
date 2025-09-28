
document.addEventListener("DOMContentLoaded", function () {
    const formEliminar = document.getElementById("EliminarReservacionForm");

    if (!formEliminar) return;

    formEliminar.addEventListener("submit", function (e) {
        e.preventDefault();

        Swal.fire({
            title: "¿Está seguro?",
            text: "La Reservacion será eliminada permanentemente.",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Sí, eliminar",
            cancelButtonText: "Cancelar",
            reverseButtons: true
        }).then(result => {
            if (result.isConfirmed) formEliminar.submit();
        });
    });
});


document.addEventListener("DOMContentLoaded", function () {
    const formModificar = document.getElementById("ModificarReservacionForm");

    if (!formModificar) return;

    formModificar.addEventListener("submit", function (e) {
        e.preventDefault();

        const idmesa = document.getElementById("IdMesa").value.trim();
        const idnumero = document.getElementById("IdNumeroDeMesa").value.trim();
        const idusuario = document.getElementById("IdUsuario").value.trim();
        const fecha = document.getElementById("FechaDeReservacion").value.trim();
      


        if (!idmesa || !idnumero || !idusuario || !fecha {
            Swal.fire({
                icon: "warning",
                title: "Campos incompletos",
                text: "Complete todos los campos antes de modificar."
            });
            return;
        }

        Swal.fire({
            title: "¿Desea modificar esta reservacion?",
            text: "Se Modificara la reservacion seleccionada.",
            icon: "question",
            showCancelButton: true,
            confirmButtonText: "Sí, modificar",
            cancelButtonText: "Cancelar",
            reverseButtons: true
        }).then(result => {
            if (result.isConfirmed) formModificar.submit();
        });
    });
});
