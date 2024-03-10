// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function changeColorOnOver() {
    var element = document.getElementById("test")
    element.className = "nav-link text-black";
}

function changeColorOnOut() {
    var element = document.getElementById("test")
    element.className = "nav-link text-white";
}
