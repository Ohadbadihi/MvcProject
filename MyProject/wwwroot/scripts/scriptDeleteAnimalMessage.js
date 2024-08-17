function handleMessage(messageId) {
    let message = document.getElementById(messageId);
    if (message) {
        message.style.display = "block";
        setTimeout(function () {
            message.style.opacity = "0";
            setTimeout(function () {
                message.style.display = "none";
            }, 300);
        }, 3000);
    }
}

handleMessage("successMessage");
handleMessage("errorMessage");