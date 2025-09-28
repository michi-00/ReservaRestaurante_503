document.addEventListener("DOMContentLoaded", function () {
    const formEliminar = document.getElementById("EliminarNumeroDeMesaForm");

    if (!formEliminar) return;

    formEliminar.addEventListener("submit", function (e) {
        e.preventDefault();

        Swal.fire({
            title: "¿Está seguro?",
            text: "El numero de mesa será eliminado permanentemente.",
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
    const formGuardar = document.getElementById("GuardarNumeroDeMesaForm");

    if (formGuardar) {
        formGuardar.addEventListener("submit", function (e) {
            e.preventDefault();

            const nombre = document.getElementById("Nombre").value.trim();

            if (!nombre) {
                Swal.fire({
                    icon: "warning",
                    title: "Campos incompletos",
                    text: "Por favor, complete todos los campos antes de guardar."
                });
                return;
            }

            Swal.fire({
                title: "¿Está seguro?",
                text: "Se guardará el numero de mesa digitada.",
                icon: "question",
                showCancelButton: true,
                confirmButtonText: "Sí, guardar",
                cancelButtonText: "Cancelar",
                reverseButtons: true
            }).then(result => {
                if (result.isConfirmed) formGuardar.submit();
            });
        });
    }
});
document.addEventListener("DOMContentLoaded", function () {
    const formModificar = document.getElementById("ModificarNumeroDeMesaForm");

    if (!formModificar) return;

    formModificar.addEventListener("submit", function (e) {
        e.preventDefault();

        const nombre = document.getElementById("Nombre").value.trim();


        if (!nombre) {
            Swal.fire({
                icon: "warning",
                title: "Campos incompletos",
                text: "Complete todos los campos antes de modificar."
            });
            return;
        }

        Swal.fire({
            title: "¿Desea modificar este rol?",
            text: "Se Modificara el numero de mesa seleccionado.",
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
