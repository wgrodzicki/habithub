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
    //let viewRecordsTable = document.getElementById("table-view-records");
    //let rowIndex = 0;
    //let editRecordTable = document.getElementById("table-edit-record");
    //const editRecordButton = document.getElementsByClassName("edit-button");

    //for (let i = 0; i < editRecordButton.length; i++) {

    //    editRecordButton[i].addEventListener("click", (event) => {

    //        rowIndex = event.target.id;

    //        //editRecordTable

    //    });
    //}

    let recordsTable = document.getElementById("table-view-records");
    const editButtons = recordsTable.getElementsByTagName("button");

    for (let i = 0; i < editButtons.length; i++) {
        editButtons[i].addEventListener("click", (event) => {

            let editRecordRow = event.target.id;
            document.getElementById("habit-dropdown-button").innerHTML = recordsTable.rows[editRecordRow].cells[0].innerHTML;
            document.getElementById("edit-record-amount").value = recordsTable.rows[editRecordRow].cells[1].innerHTML;
            document.getElementById("edit-record-unit").value = recordsTable.rows[editRecordRow].cells[2].innerHTML;

            // From: Fri Mar 01 2024 17:57:00 GMT+0100 (czas środkowoeuropejski standardowy)
            // To: YYYY - MM - DDTHH: mm


            let rawDate = new Date(recordsTable.rows[editRecordRow].cells[3].innerHTML);
            let convertedDate = convertToDatetimeLocal(rawDate);

            document.getElementById("edit-record-date").value = convertedDate;

        });
    }
}

function convertToDatetimeLocal(dateToConvert) {
    let convertedDate;

    let year = dateToConvert.getFullYear();
    let month = dateToConvert.getMonth();
    month++;
    month = formatWithPrecedingZero(month);
    let day = dateToConvert.getDate();
    day = formatWithPrecedingZero(day);
    let hour = dateToConvert.getHours();
    hour = formatWithPrecedingZero(hour);
    let minutes = dateToConvert.getMinutes();
    minutes = formatWithPrecedingZero(minutes);

    convertedDate = `${year}-${month}-${day}T${hour}:${minutes}`;

    return convertedDate;
}

function formatWithPrecedingZero(itemToFormat) {
    if (String(itemToFormat).length >= 2) {
        return itemToFormat;
    }

   return `0${itemToFormat}`;
}