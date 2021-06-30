
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*
document.addEventListener("DOMContentLoaded", function () {
    window.addEventListener('scroll', function () {
        if (window.scrollY > 55) {
            document.getElementById('navbar_top').classList.add('fixed-top');
            //document.getElementById('navbar_top').classList.add('shadow-sm');
            // add padding top to show content behind navbar
            navbar_height = document.querySelector('.navbar').offsetHeight;
            document.body.style.paddingTop = navbar_height + 'px';
        } else {
            document.getElementById('navbar_top').classList.remove('fixed-top');
            //document.getElementById('navbar_top').classList.remove('shadow-sm');
            // remove padding top from body
            document.body.style.paddingTop = '0';
        }
    });
});*/
// DOMContentLoaded  end

$(".viewbutton").on("click", function () {
    var customerId = $(this).data('id');
    var secao = $(this).data('secao');
    var acao = $(this).data('acao');
    $.ajax({
        url: "/" + secao + "/" + acao + "?id=" + customerId,
        cache: false
    }).done(function (data) {
        $("#viewmodalData").html(data);
    });
});

$(document).ready(function () {

    $(".dropdown-trigger").dropdown();

});

document.addEventListener('DOMContentLoaded', function () {
    var elemsselect = document.querySelectorAll('select');
    var elemscollapsible = document.querySelectorAll('.collapsible');
    var instances = M.FormSelect.init(elemscollapsible, options);
    var instances = M.Collapsible.init(elemsselect, options);

    var elemssidenav = document.querySelectorAll('.sidenav');
    var instances = M.Sidenav.init(elemssidenav, options);
});

// Or with jQuery

$(document).ready(function () {
    $('select').formSelect();
});

// Or with jQuery

$(document).ready(function () {
    $('.collapsible').collapsible();
});

document.addEventListener('DOMContentLoaded', function () {

});

// Initialize collapsible (uncomment the lines below if you use the dropdown variation)
// var collapsibleElem = document.querySelector('.collapsible');
// var collapsibleInstance = M.Collapsible.init(collapsibleElem, options);

// Or with jQuery

$(document).ready(function () {
    $('.sidenav').sidenav();
});

$('.collapsible').collapsible();
$('.collapsible.expandable').collapsible({
    accordion: false
});