jQuery(function () {
    const form = $("#formEditAnimal");
    const submitButton = $("#submitButton");

    function updateSubmitButtonState() {
        const allFieldsValid = form.valid();
        submitButton.prop("disabled", !allFieldsValid);
    }

    $.validator.addMethod("notOnlyWhitespace", function (value) {
        return /\S/.test(value);
    }, "This field cannot be empty or contain only whitespace.");

    $.validator.addMethod("validName", function (value) {
        return /^[a-zA-Z][a-zA-Z\s]*$/.test(value);
    }, "Name should start with a letter and contain only letters and spaces.");

    $.validator.addMethod("noForbiddenWords", function (value) {
        const forbiddenWords = ["database", "delete", "table", "column", "row"];
        const regex = new RegExp('\\b(' + forbiddenWords.join('|') + ')\\b', "i");
        return !regex.test(value);
    }, "This field contains forbidden words.");

    $.validator.addMethod("maxlength", function (value, element, param) {
        return this.optional(element) || value.length <= param;
    }, "Please enter no more than {0} characters.");

    form.validate({
        rules: {
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
                notOnlyWhitespace: true,
                number: true,
                min: 1,
                max: 100,
                step: 1
            },
            Description: {
                required: true,
                notOnlyWhitespace: true,
                minlength: 20,
                maxlength: 630,
                noForbiddenWords: true
            },
            CategoryId: {
                required: true,
                notOnlyWhitespace: true
            }
        },
        errorPlacement: function (error, element) {
            $("#" + element.attr("name") + "Error").html(error).show();
            element.addClass("error-input");
        },
        success: function (label, element) {
            $(element).removeClass("error-input");
            $("#" + $(element).attr("name") + "Error").html("").hide();
        }
    });

    // Validate on input, change, and blur events
    $("input, select, textarea").on('input change blur', function () {
        const isValid = $(this).valid();
        updateSubmitButtonState();
        if (!isValid) {
            $(this).addClass("error-input");
            $("#" + $(this).attr("name") + "Error").show();
        } else {
            $(this).removeClass("error-input");
            $("#" + $(this).attr("name") + "Error").hide();
        }
    });

    // Prevent form submission if invalid
    form.on('submit', function (e) {
        if (!form.valid()) {
            e.preventDefault();
            updateSubmitButtonState();
        }
    });

    // Delay initial validation
    setTimeout(function () {
        form.valid();
        updateSubmitButtonState();
    }, 500);
});
