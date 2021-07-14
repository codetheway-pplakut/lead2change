// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showHideClass(id1) {
    var x = document.getElementsByClassName(id1);
    for (let i = 0; i < x.length; i++) {
        if (x[i].style.display !== "none") {
            x[i].style.display = "none";
        } else {
            x[i].style.display = "block";
        }
    }
}

function showHideClass2(id2) {
    var x = document.getElementsByClassName(id2);
    for (let i = 0; i < x.length; i++) {
        if (x[i].style.display !== "none") {
            x[i].style.display = "none";
        } else {
            x[i].style.display = "block";
        }
    }
}
