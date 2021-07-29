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

$(".hide").click(function () {
    $("knowCounselor").hide();
})

$(".show").click(function () {
    $("knowCounselor").show();
})

function search() {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("interviewSearch");
    filter = input.value.toUpperCase();
    table = document.getElementById("interviewStudentTable");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}