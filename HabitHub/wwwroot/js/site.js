// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {
    const navbarPills = document.getElementsByClassName("nav-link text-white");

    for (let i = 0; i < navbarPills.length; i++) {
        navbarPills[i].addEventListener("mouseover", (event) => {
            event.target.className = "nav-link text-black";
        });

        navbarPills[i].addEventListener("mouseout", (event) => {
            event.target.className = "nav-link text-white";
        });
    }
});
