import { HttpClient } from '../shared/http-client.js';
import { setFormErrors, disableSubmit, enableSubmit } from '../shared/form-utils.js';

const http = new HttpClient();

/**
 * Handles the login form submission.
 * @param {HTMLFormElement} form
 */
export async function handleLogin(form) {
    disableSubmit(form);
    try {
        const formData = new FormData(form);
        const response = await http.postForm('/Auth/Login', formData);
        if (response?.redirectUrl) {
            window.location.href = response.redirectUrl;
        }
    } catch (error) {
        if (error.data?.details) {
            setFormErrors(form, error.data.details);
        }
    } finally {
        enableSubmit(form);
    }
}
