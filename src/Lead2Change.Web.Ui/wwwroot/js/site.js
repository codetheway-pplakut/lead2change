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

function hideClass(id1) {
    var x = document.getElementsByClassName(id1);
    for (let i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
}

function showClass(id1) {
    var x = document.getElementsByClassName(id1);
    for (let i = 0; i < x.length; i++) {
        x[i].style.display = "block";
    }
}

function handleRegisterQuestions(plan) {
    hideClass("collegeQuestions");
    hideClass("tradeSchoolQuestions");
    hideClass("armedForcesQuestions");
    hideClass("workQuestions");
    hideClass("otherQuestions");
    if (plan == "College") {
        showClass("collegeQuestions");
    }
    else if (plan == "Trade School") {
        showClass("tradeSchoolQuestions");
    }
    else if (plan == "Armed Forces") {
        showClass("armedForcesQuestions");
    }
    else if (plan == "Work") {
        showClass("workQuestions");
    }
    else if(plan == "Other"){
        showClass("otherQuestions");
    }
    
}

$(".hide").click(function () {
    $("knowCounselor").hide();
})

$(".show").click(function () {
    $("knowCounselor").show();
})

function searchFilter() {
    var input, filter, table, tr, td, cell, i, j;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");
    for (i = 1; i < tr.length; i++) {
        // Hide the row initially.
        tr[i].style.display = "none";

        td = tr[i].getElementsByTagName("td");
        for (var j = 0; j < td.length; j++) {
            cell = tr[i].getElementsByTagName("td")[j];
            if (cell) {
                if (cell.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                    break;
                }
            }
        }
    }
}