import { setFormErrors, clearFormErrors, disableSubmit, enableSubmit } from '../shared/form-utils.js';
import { isValidEmail, isStrongPassword, isValidUsername } from '../shared/validation.js';

/**
 * Validates the registration form on the client side.
 * @param {HTMLFormElement} form
 * @returns {boolean}
 */
export function validateRegisterForm(form) {
    clearFormErrors(form);
    const errors = {};

    const email = form.querySelector('[name="Email"]')?.value;
    if (email && !isValidEmail(email)) {
        errors['Email'] = 'Please enter a valid email address.';
    }

    const username = form.querySelector('[name="Username"]')?.value;
    if (username && !isValidUsername(username)) {
        errors['Username'] = 'Username must be 3-50 alphanumeric characters or underscores.';
    }

    const password = form.querySelector('[name="Password"]')?.value;
    const confirmPassword = form.querySelector('[name="ConfirmPassword"]')?.value;
    if (password && confirmPassword && password !== confirmPassword) {
        errors['ConfirmPassword'] = 'Passwords do not match.';
    }

    if (Object.keys(errors).length > 0) {
        setFormErrors(form, errors);
        return false;
    }

    return true;
}
