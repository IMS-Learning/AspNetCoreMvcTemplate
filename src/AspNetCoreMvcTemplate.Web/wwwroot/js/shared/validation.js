/**
 * Client-side validation utilities.
 */

export const patterns = {
    email: /^[^@\s]+@[^@\s]+\.[^@\s]+$/,
    username: /^[a-zA-Z0-9_]{3,50}$/,
    phone: /^\+?[1-9]\d{1,14}$/,
    strongPassword: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$/
};

export function isValidEmail(email) {
    return patterns.email.test(email);
}

export function isValidUsername(username) {
    return patterns.username.test(username);
}

export function isStrongPassword(password) {
    return patterns.strongPassword.test(password);
}

/**
 * Validates a form element and returns errors.
 * @param {HTMLFormElement} form
 * @returns {Object} errors
 */
export function validateForm(form) {
    const errors = {};
    const inputs = form.querySelectorAll('[required]');

    inputs.forEach(input => {
        const name = input.name;
        const value = input.value.trim();

        if (!value) {
            errors[name] = `${input.dataset.label || name} is required.`;
            return;
        }

        if (input.type === 'email' && !isValidEmail(value)) {
            errors[name] = 'Please enter a valid email address.';
        }

        if (input.dataset.minLength && value.length < parseInt(input.dataset.minLength)) {
            errors[name] = `Must be at least ${input.dataset.minLength} characters.`;
        }
    });

    return errors;
}
