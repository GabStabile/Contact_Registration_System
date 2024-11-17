// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Add evento de clique aos elementos com a classe 'close-alert'
// Responsável por esconder os elementos de alerta da página
$('.close-alert').click(function () {
    // Oculta todos os elementos com a classe 'alert' quando o botão de fechar é clicado
    $('.alert').hide('hide');
})

$(document).ready(function () {
    $('#table-contacts').DataTable();
});

$(document).ready(function () {
    $('#table-users').DataTable();
});