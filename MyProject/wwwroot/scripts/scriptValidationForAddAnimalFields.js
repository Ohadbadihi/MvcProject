jQuery(function ($) {
    const form = $("#formAddAnimal");
    const submitButton = $("#submitButton");
    let formInteracted = false;

    // Custom validation methods
    const customValidators = {
        notOnlyWhitespace: {
            method: value => /\S/.test(value),
            message: "This field cannot be empty or contain only whitespace."
        },
        validName: {
            method: value => /^[a-zA-Z][a-zA-Z\s]{1,48}$/.test(value),
            message: "Name should start with a letter and contain only letters and spaces (2-50 characters)."
        },
        validPicture: {
            method: (value, element) => {
                if (element.files.length === 0) return false;
                const fileExtension = element.files[0].name.split('.').pop().toLowerCase();
                return ['jpg', 'jpeg', 'png'].includes(fileExtension);
            },
            message: "Please select a valid JPG, JPEG, or PNG file."
        },
        noForbiddenWords: {
            method: value => {
                const forbiddenWords = ["database", "delete", "table", "column"];
                const regex = new RegExp(forbiddenWords.join("|"), "i");
                return !regex.test(value);
            },
            message: "This field contains forbidden words."
        }
    };

    // Add custom validators to jQuery validator
    Object.entries(customValidators).forEach(([name, { method, message }]) => {
        $.validator.addMethod(name, method, message);
    });

    // Validation rules and messages
    const validationRules = {
        Name: {
            required: true,
            notOnlyWhitespace: true,
            minlength: 2,
            maxlength: 50,
            validName: true,
            noForbiddenWords: true
        },
        Age: {
            required: true,
            number: true,
            min: 1,
            max: 100
        },
        Description: {
            required: true,
            notOnlyWhitespace: true,
            minlength: 20,
            maxlength: 630,
            noForbiddenWords: true
        },
        CategoryId: {
            required: true
        },
        PictureFile: {
            required: true,
            validPicture: true
        }
    };

    const validationMessages = {
        Name: {
            required: "Please enter the animal's name.",
            minlength: "Name must be at least 2 characters long.",
            maxlength: "Name cannot exceed 50 characters."
        },
        Age: {
            required: "Please enter the animal's age.",
            number: "Age must be a number.",
            min: "Age must be at least 1.",
            max: "Age cannot exceed 100."
        },
        Description: {
            required: "Please provide a description.",
            minlength: "Description must be at least 20 characters long.",
            maxlength: "Description cannot exceed 630 characters."
        },
        CategoryId: {
            required: "Please select a category."
        },
        PictureFile: {
            required: "Please upload a picture."
        }
    };

    // Initialize form validation
    form.validate({
        rules: validationRules,
        messages: validationMessages,
        errorPlacement: (error, element) => {
            $(`#${element.attr("name")}Error`).html(error);
            element.addClass("error-input");
        },
        success: (label, element) => {
            $(element).removeClass("error-input");
            $(`#${$(element).attr("name")}Error`).html("");
        },
        onkeyup: true,
        onfocusout: true,
        onclick: true
    });

    // Update submit button state
    function updateSubmitButtonState() {
        submitButton.prop("disabled", !formInteracted || !form.valid());
    }

    // Handle form interaction
    $("input, select, textarea").on('focusin', () => {
        formInteracted = true;
    }).on('input change blur', function () {
        if (formInteracted) {
            $(this).valid();
            updateSubmitButtonState();
        }
    });

    // Handle form submission
    form.on('submit', function (e) {
        e.preventDefault();
        if (form.valid()) {
            submitButton.prop('disabled', true)
                .html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Submitting...');
            this.submit();
        }
    });

    // Initial state of submit button
    updateSubmitButtonState();
});