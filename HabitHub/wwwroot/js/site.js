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
        populateEditModal();
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
        // Don't clean the warning
        if (document.title == "Add habit - HabitHub") {
            if (document.getElementById("habit-warning").innerHTML != "") {
                continue;
            }
        }
        forms[i].value = null;
    }
}

// Retrieves habit name from a dropdown menu.
function getHabitFromDropdown() {
    if (document.title == "View habits - HabitHub") {

        // Filter by habit dropdown
        const dropdownItemsFilter = document.getElementById("habit-dropdown-filter").getElementsByClassName("dropdown-item");

        for (let i = 0; i < dropdownItemsFilter.length; i++) {
            dropdownItemsFilter[i].addEventListener("click", (event) => {
                document.getElementById("habit-dropdown-button-filter").innerHTML = event.target.innerHTML;
                document.getElementById("habit-input-filter").value = event.target.innerHTML;
            });
        }

        // Modal habit dropdown
        const dropdownItemsModal = document.getElementById("table-edit-record").getElementsByClassName("dropdown-item");

        for (let i = 0; i < dropdownItemsModal.length; i++) {
            dropdownItemsModal[i].addEventListener("click", (event) => {
                document.getElementById("habit-dropdown-button").innerHTML = event.target.innerHTML;
                document.getElementById("habit-input").value = event.target.innerHTML;
            });
        }
    }
    else {
        // Regular habit dropdown
        const dropdownItems = document.getElementsByClassName("dropdown-item");

        for (let i = 0; i < dropdownItems.length; i++) {
            dropdownItems[i].addEventListener("click", (event) => {
                document.getElementById("habit-dropdown-button").innerHTML = event.target.innerHTML;
                document.getElementById("habit-input").value = event.target.innerHTML;
            });
        }
    }
}

// Populates the edit modal with the currently selected record data.
function populateEditModal() {
    let recordsTable = document.getElementById("table-view-records");
    const editButtons = recordsTable.getElementsByTagName("button");

    for (let i = 0; i < editButtons.length; i++) {
        editButtons[i].addEventListener("click", (event) => {

            let editRecordRow = event.target.id;

            document.getElementById("edit-record-id").value = editButtons[i].value;
            document.getElementById("habit-dropdown-button").innerHTML = recordsTable.rows[editRecordRow].cells[0].innerHTML;
            document.getElementById("edit-record-amount").value = recordsTable.rows[editRecordRow].cells[1].innerHTML;
            document.getElementById("edit-record-unit").value = recordsTable.rows[editRecordRow].cells[2].innerHTML;

            let rawDate = new Date(recordsTable.rows[editRecordRow].cells[3].innerHTML);
            let convertedDate = convertToDatetimeLocal(rawDate);

            document.getElementById("edit-record-date").value = convertedDate;

        });
    }
}

// Converts the given date to the YYYY-MM-DDTHH:MM format.
function convertToDatetimeLocal(dateToConvert) {
    let year = dateToConvert.getFullYear();

    let month = dateToConvert.getMonth();
    month++; // Account for zero-indexing
    month = padWithZero(month);

    let day = dateToConvert.getDate();
    day = padWithZero(day);

    let hour = dateToConvert.getHours();
    hour = padWithZero(hour);

    let minutes = dateToConvert.getMinutes();
    minutes = padWithZero(minutes);

    let convertedDate = `${year}-${month}-${day}T${hour}:${minutes}`;

    return convertedDate;
}

// Pads the given string with a leading 0 if single-digit.
function padWithZero(itemToFormat) {
    if (String(itemToFormat).length >= 2) {
        return itemToFormat;
    }
    return `0${itemToFormat}`;
}