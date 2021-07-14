// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showHideClass(id1) {
    var x = document.getElementsByClassName(id1);
    for (let i = 0; i < x.length; i++) {
        if (x[i].style.display == "block") {
            x[i].style.display = "none";
        } else {
            x[i].style.display = "block";
        }
    }
}

$(".showHide").on("click", function () {

    if () {

    } else {

    }

    $(".showHide").toggle(
        function () { $(".showHide").css({ "display": "none" }); },
        function () { $("showHide").css({ "display": "block" }) }
    );
});
