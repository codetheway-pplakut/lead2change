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

function checkerShowHide(id1) {
    var y = document.getElementById("checkboxTest").checked;
    var x = document.getElementsByClassName(id1);
    if (y) {
        for (let i = 0; i < x.length; i++) {
            x[i].style.display = "block";
        }
    } else {
        for (let i = 0; i < x.length; i++) {
            x[i].style.display = "none";
        }
    }
}

function checkBoxCheck() {
    if (checkBox.checked) {

    }
}

$(".hide").click(function () {
    $("knowCounselor").hide();
})

$(".show").click(function () {
    $("knowCounselor").show();
})

