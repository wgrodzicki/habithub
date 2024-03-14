// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {
    changeNavbarPillColor();

    if (document.title == "Record habit - HabitHub" || document.title == "Add habit - HabitHub") {
        getHabitFromDropdown();
    }

});

// Changes navbar pill color on hover.
function changeNavbarPillColor() {
    const navbarPills = document.getElementsByClassName("nav-link text-white");

    for (let i = 0; i < navbarPills.length; i++) {
        navbarPills[i].addEventListener("mouseover", (event) => {
            event.target.className = "nav-link text-black";
        });

        navbarPills[i].addEventListener("mouseout", (event) => {
            event.target.className = "nav-link text-white";
        });
    }
}

// Retrieves habit name from a dropdown menu.
function getHabitFromDropdown() {
    const dropdownItems = document.getElementsByClassName("dropdown-item");

    for (let i = 0; i < dropdownItems.length; i++) {
        dropdownItems[i].addEventListener("click", (event) => {
            document.getElementById("habit-dropdown-button").innerHTML = event.target.innerHTML;
            document.getElementById("habit-input").value = event.target.innerHTML;
        });
    }
}
