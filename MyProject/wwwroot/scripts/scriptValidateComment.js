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


//jQuery(function ($) {
//    const form = $(".form-AddComment");
//    const commentText = $("#commentText");
//    const addCommentBtn = $("#CommentBtn");
//    const messageBox = $(".messageBox");

//    // Custom validation methods
//    const customValidators = {
//        notOnlyWhitespace: {
//            method: value => /\S/.test(value),
//            message: "Comment cannot consist only of whitespace."
//        },
//        validLength: {
//            method: value => value.length >= 2 && value.length <= 120,
//            message: "Comment must be between 2 and 120 characters long."
//        },
//        noForbiddenWords: {
//            method: value => {
//                const forbiddenWords = ["DELETE", "TABLE", "DATABASE"];
//                const regex = new RegExp(forbiddenWords.join("|"), "i");
//                return !regex.test(value);
//            },
//            message: "Comment contains forbidden words."
//        }
//    };

//    // Add custom validators to jQuery validator
//    Object.entries(customValidators).forEach(([name, { method, message }]) => {
//        $.validator.addMethod(name, method, message);
//    });

//    // Validation rules and messages
//    const validationRules = {
//        comment: {
//            required: true,
//            notOnlyWhitespace: true,
//            validLength: true,
//            noForbiddenWords: true
//        }
//    };

//    const validationMessages = {
//        comment: {
//            required: "Comment cannot be empty."
//        }
//    };

//    // Initialize form validation
//    form.validate({
//        rules: validationRules,
//        messages: validationMessages,
//        errorPlacement: (error, element) => {
//            messageBox.html(error);
//            element.addClass("error-input");
//        },
//        success: (label, element) => {
//            $(element).removeClass("error-input");
//            messageBox.html("");
//        }
//    });

//    // Update submit button state
//    function updateSubmitButtonState() {
//        addCommentBtn.prop("disabled", !form.valid());
//    }

//    // Disable button initially
//    addCommentBtn.prop("disabled", true);

//    // Handle input changes
//    commentText.on('input', function () {
//        $(this).valid();  // Trigger validation
//        updateSubmitButtonState();
//    });

//    // Handle form submission
//    form.on('submit', function (e) {
//        e.preventDefault();
//        if (form.valid()) {
//            addCommentBtn.prop('disabled', true)
//                .html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Submitting...');
//            messageBox.css('color', 'green').text("Comment has been added successfully.");
//            setTimeout(() => {
//                this.submit();
//            }, 800);
//        }
//    });
//});
