document.addEventListener("DOMContentLoaded", function () {
    const popoverTriggerList = document.querySelectorAll(".description-popover");

    popoverTriggerList.forEach(function (popoverTriggerEl) {
        new bootstrap.Popover(popoverTriggerEl, {
            html: true
        });
    });
});