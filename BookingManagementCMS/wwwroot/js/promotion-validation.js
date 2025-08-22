//Validate promotion form fileds
function ValidatePromotions() {
    let isValid = true;

    // Clear previous errors
    $('#name-Error, #startDate-Error, #endDate-Error').text('').hide();
    //Name validation
    if (!validateName()) {
        isValid = false;
    }
    //Couponcode validation
    if (!validateCouponcode()) {
        isValid = false;
    }
    //Start Date validation
    if (!validateStartDate()) {
        isValid = false;
    }
    //End Date validation
    if (!validateEndDate()) {
        isValid = false;
    }
    // Compare dates
    if (!CompareDate()) {
        isValid = false;
    }

    return isValid;
}
//Validate Name
const validateName = () => {
    try {
        const name = $('#name').val() || ''
        const $error = $('#name-Error');
        let isValid = true;

        if (!name) {
            $error.text('This field is required.').show();
            isValid = false;
        } else {
            $error.text('');
        }

        return isValid;
    } catch (e) {
        console.error('Validation error:', e.message);
        return false;
    }
};
//Validate Couponcode
const validateCouponcode = () => {
    try {
        const couponcode = $('#couponcode').val() || ''
        const $error = $('#couponcode-Error');
        let isValid = true;

        if (!couponcode) {
            $error.text('This field is required.').show();
            isValid = false;
        } else {
            $error.text('');
        }

        return isValid;
    } catch (e) {
        console.error('Validation error:', e.message);
        return false;
    }
};
//Validate Start date
const validateStartDate = () => {
    try {
        const startDate = $('#startDate').val() || ''
        const $error = $('#startDate-Error');
        let isValid = true;

        if (!startDate) {
            $error.text('This field is required.').show();
            isValid = false;
        } else {
            $error.text('').hide();
        }

        return isValid;
    } catch (e) {
        console.error('Validation error:', e.message);
        return false;
    }
};
//Validate Start date
const validateEndDate = () => {
    try {
        const endDate = $('#endDate').val() || ''
        const $error = $('#endDate-Error');
        let isValid = true;

        if (!endDate) {
            $error.text('This field is required.').show();
            isValid = false;
        } else {
            $error.text('').hide();
        }

        return isValid;
    } catch (e) {
        console.error('Validation error:', e.message);
        return false;
    }
};
//Compare dates
const CompareDate = () => {
    try {
        const startDate = $('#startDate').val() || ''
        const endDate = $('#endDate').val() || ''
        const $error = $('#endDate-Error');
        let isValid = true;

        if (startDate && endDate) {
            const [startDay, startMonth, startYear] = startDate.split('/');
            const start = new Date(startYear, startMonth - 1, startDay);

            const [endDay, endMonth, endYear] = endDate.split('/');
            const end = new Date(endYear, endMonth - 1, endDay);

            if (end < start) {
                $error.text('End date must be greater than or equal to the start date.').show();
                isValid = false;
            } else {
                $error.text('').hide();
            }
        }

        return isValid;
    } catch (e) {
        console.error('Validation error:', e.message);
        return false;
    }
};


//Clear the all form fields
function ClearPromotionForm(formSelector) {
    const $form = $(formSelector);

    // Clear text inputs, password, number, email, and textareas
    $form.find('input[type="text"], input[type="password"], input[type="number"], input[type="email"], textarea')
        .val('');

    // Uncheck checkboxes and radios
    $form.find('input[type="checkbox"], input[type="radio"]')
        .prop('checked', false);

    // Reset selects
    $form.find('select')
        .prop('selectedIndex', 0);

    // Clear file inputs and hide associated images
    $form.find('input[type="file"]').each(function () {
        $(this).val('').siblings('img').hide();
    });

    // Optionally hide all images inside the form
    $(formSelector).find('img').hide();

    // Optional: Hide error messages
    $('.text-danger').text('').hide();
}
