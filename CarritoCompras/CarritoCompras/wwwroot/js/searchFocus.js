window.addEventListener("DOMContentLoaded", () => {
    const input = document.getElementById("filtroBusqueda");

    if (input) {
        // Guardar la posición del cursor cuando el usuario escriba
        input.addEventListener("input", function () {
            localStorage.setItem("caretPosition", input.selectionStart);
        });

        // Restaurar la posición del cursor después de que el formulario se haya recargado
        setTimeout(() => {
            var caretPosition = localStorage.getItem("caretPosition");
            if (caretPosition !== null) {
                input.setSelectionRange(caretPosition, caretPosition);
            }

            // Foco en el campo de búsqueda
            input.focus();
        }, 100); // 100 ms para esperar a que la página se recargue
    }
});