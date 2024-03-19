// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {
    changeNavbarPillColor();
    cleanForms();

    if (document.title == "Record habit - HabitHub"
        || document.title == "Add habit - HabitHub"
        || document.title == "View habits - HabitHub") {
        getHabitFromDropdown();
    }

    if (document.title == "View habits - HabitHub") {
        populateModal();
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

// Cleans all forms on loading.
function cleanForms() {
    const forms = document.getElementsByClassName("form-control");

    for (let i = 0; i < forms.length; i++) {
        forms[i].value = null;
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

function populateModal() {
    let viewRecordsTable = document.getElementById("table-view-records");
    let rowIndex = 0;
    let editRecordTable = document.getElementById("table-edit-record");
    const editRecordButton = document.getElementsByClassName("edit-button");

    for (let i = 0; i < editRecordButton.length; i++) {

        editRecordButton[i].addEventListener("click", (event) => {

            rowIndex = event.target.id;

            //editRecordTable

        });
    }


    
}
