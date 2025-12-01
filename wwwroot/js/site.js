// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function seleccionarInstrumento(instrumento) {
        // Aquí puedes agregar la lógica para manejar la selección del instrumento
        console.log('Instrumento seleccionado:', instrumento);
        // Ejemplo de redirección (ajusta según tu ruta)
        // window.location.href = `/Aprender/${instrumento}`;
        
        // Muestra un mensaje temporal (puedes personalizar esto)
        const mensaje = `¡Has seleccionado ${instrumento.charAt(0).toUpperCase() + instrumento.slice(1)}!`;
        alert(mensaje);
    }

// Toggle menu function
function toggleMenu() {
    // Add menu toggle functionality here
    console.log("Menu toggled");
    // You can add your menu toggle logic here
}