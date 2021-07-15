
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
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
});

$(document).ready(function () {
    // the "href" attribute of .modal-trigger must specify the modal ID that wants to be triggered
    // the "data-source" attribute of .modal-trigger must specify the url that will be ajaxed
    //$('.modal-trigger').click(function () {

        //var customerId = $(this).data('id');
        //var secao = $(this).data('secao');
        //var acao = $(this).data('acao');

        //var url = '/' + secao + '/' + acao + '?id=' + customerId;
        // use other ajax submission type for post, put ...
        //$.get(url, function (data) {
            // use this method you need to handle the response from the view 
            // with rails Server-Generated JavaScript Responses this is portion will be in a .js.erb file  
            //$(".modal-content").html(data);
        //});

        //$.ajax({
            //url: "/" + secao + "/" + acao + "?id=" + customerId,
            //cache: true
        //}).done(function (data) {
           // $(".modal-content").html(data);
        //});
    //});
    // opens the modal
});

$(document).ready(function () {
    $('.modal').modal();

    $('.dropdown-trigger').dropdown();

    $('select').formSelect();

    $('.collapsible').collapsible();

    $('.sidenav').sidenav();

    $('.tabs').tabs();

    $('.datepicker').datepicker();    
});

