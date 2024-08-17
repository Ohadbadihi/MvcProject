document.addEventListener('DOMContentLoaded', function () {
    const $form = $(".form-AddComment");
    const $commentText = $("#commentText");
    const $messageBox = $(".messageBox");
    const $addCommentBtn = $(".addCommentBtn");

    // Disable the button by default
    $addCommentBtn.prop('disabled', true);

    // Live validation
    $commentText.on('input', validateComment);

    // Use on('submit') instead of submit()
    $form.on('submit', function (event) {
        event.preventDefault();
        if (validateComment()) {
            $messageBox.css('color', 'green').text("Comment has been added successfully.");
            $addCommentBtn.prop('disabled', true);
            setTimeout(() => {
                // Use get(0) to access the native form element and call submit()
                $form.off('submit').get(0).submit();
            }, 800);
        }
    });

    function validateComment() {
        const comment = $commentText.val().trim();
        if (comment === "") {
            showErrorMessage("Comment cannot be empty.");
            return false;
        }
        if (comment.length < 2) {
            showErrorMessage("Comment must be at least 2 characters long.");
            return false;
        }
        if (comment.length > 120) {
            showErrorMessage("Comment must not exceed 120 characters.");
            return false;
        }
        if (/^\s+$/.test(comment)) {
            showErrorMessage("Comment cannot consist only of whitespace.");
            return false;
        }
        const forbiddenWords = ["DELETE", "TABLE", "DATABASE"];
        if (forbiddenWords.some(word => comment.toUpperCase().includes(word))) {
            showErrorMessage("Comment contains forbidden words.");
            return false;
        }
        // If all validations pass
        $messageBox.text(""); // Clear any previous error messages
        $addCommentBtn.prop('disabled', false);
        return true;
    }

    function showErrorMessage(message) {
        $messageBox.css('color', 'red').text(message);
        $addCommentBtn.prop('disabled', true);
    }
});

