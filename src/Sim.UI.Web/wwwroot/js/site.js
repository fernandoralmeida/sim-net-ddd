
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(".viewbutton").on("click", function () {
    var customerId = $(this).data('id');
    var secao = $(this).data('secao');
    var acao = $(this).data('acao');
    $.ajax({
        url: "/" + secao + "/" + acao + "?id=" + customerId,
        cache: true
    }).done(function (data) {
        $("#viewmodalData").html(data);
    });
});

$(document).ready(function () {
    $(".dropdown-trigger").dropdown();
});

$(document).ready(function () {
    $('select').formSelect();
});

$(document).ready(function () {
    $('.collapsible').collapsible();
});

$(document).ready(function () {
    $('.sidenav').sidenav();
});

$(document).ready(function () {
    $('.modal').modal();
});

$(document).ready(function () {
    $('.tabs').tabs();
});

