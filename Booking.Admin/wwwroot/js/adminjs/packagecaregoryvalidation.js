$(document).ready(function () {
    // Initialize form validation
    $("#packageCategory").validate({
        // Specify validation rules
        rules: {
            CategoryName: {
                required: true,
                minlength: 2,
                maxlength: 100
            },
            Description: {
                required: true,
                minlength: 10
            }
        },
        // Specify validation error messages
        messages: {
            CategoryName: {
                required: "Please enter category name",
                minlength: "Category name must be at least 2 characters",
                maxlength: "Category name cannot exceed 100 characters"
            },
            Description: {
                required: "Please enter description",
                minlength: "Description must be at least 10 characters"
            }
        },
        errorElement: "span",
        errorClass: "text-danger",
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            var $submitButton = $("#submitButton");
            var originalText = $submitButton.text();

            // Disable button and show loading state
            $submitButton.prop('disabled', true)
                .html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Submitting...');

            // Submit the form
            form.submit();
        }
    });

    // Optional: Add click handler for additional validation
    $("#submitButton").click(function (e) {
        e.preventDefault();
        if ($("#packageCategory").valid()) {
            $("#packageCategory").submit();
        }
    });
});