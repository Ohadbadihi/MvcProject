document.addEventListener("DOMContentLoaded", function () {
    const activeLink = document.querySelector(".nav-link.active");
    if (activeLink) {
        const icon = document.createElement("i");
        icon.classList.add("fa-solid", "fa-caret-up");
        activeLink.appendChild(icon);
    }
});

