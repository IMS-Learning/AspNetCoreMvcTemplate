/**
 * Reusable form utility functions.
 */

/**
 * Sets validation error messages on form fields.
 * @param {HTMLFormElement} form
 * @param {Object} errors - { fieldName: ['error1', 'error2'] }
 */
export function setFormErrors(form, errors) {
    clearFormErrors(form);
    for (const [field, messages] of Object.entries(errors)) {
        const input = form.querySelector(`[name="${field}"]`);
        if (input) {
            input.classList.add('is-invalid');
            const feedback = document.createElement('div');
            feedback.className = 'invalid-feedback';
            feedback.textContent = Array.isArray(messages) ? messages[0] : messages;
            input.parentNode.appendChild(feedback);
        }
    }
}

/**
 * Clears all validation error states from a form.
 * @param {HTMLFormElement} form
 */
export function clearFormErrors(form) {
    form.querySelectorAll('.is-invalid').forEach(el => el.classList.remove('is-invalid'));
    form.querySelectorAll('.invalid-feedback').forEach(el => el.remove());
}

/**
 * Serializes a form to a plain object.
 * @param {HTMLFormElement} form
 * @returns {Object}
 */
export function serializeForm(form) {
    const data = {};
    new FormData(form).forEach((value, key) => {
        data[key] = value;
    });
    return data;
}

/**
 * Disables all submit buttons in a form.
 * @param {HTMLFormElement} form
 */
export function disableSubmit(form) {
    form.querySelectorAll('[type="submit"]').forEach(btn => {
        btn.disabled = true;
        btn.dataset.originalText = btn.textContent;
        btn.textContent = 'Loading...';
    });
}

/**
 * Re-enables all submit buttons in a form.
 * @param {HTMLFormElement} form
 */
export function enableSubmit(form) {
    form.querySelectorAll('[type="submit"]').forEach(btn => {
        btn.disabled = false;
        if (btn.dataset.originalText) {
            btn.textContent = btn.dataset.originalText;
        }
    });
}
