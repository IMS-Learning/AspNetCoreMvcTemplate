/**
 * Reusable HTTP client for API communication.
 */
export class HttpClient {
    constructor(baseUrl = '') {
        this.baseUrl = baseUrl;
    }

    async get(url) {
        const response = await fetch(this.baseUrl + url, {
            headers: this._getHeaders()
        });
        return this._handleResponse(response);
    }

    async post(url, data) {
        const response = await fetch(this.baseUrl + url, {
            method: 'POST',
            headers: this._getHeaders(),
            body: JSON.stringify(data)
        });
        return this._handleResponse(response);
    }

    async put(url, data) {
        const response = await fetch(this.baseUrl + url, {
            method: 'PUT',
            headers: this._getHeaders(),
            body: JSON.stringify(data)
        });
        return this._handleResponse(response);
    }

    async delete(url) {
        const response = await fetch(this.baseUrl + url, {
            method: 'DELETE',
            headers: this._getHeaders()
        });
        return this._handleResponse(response);
    }

    async postForm(url, formData) {
        const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
        const response = await fetch(this.baseUrl + url, {
            method: 'POST',
            headers: token ? { 'RequestVerificationToken': token } : {},
            body: formData
        });
        return this._handleResponse(response);
    }

    _getHeaders() {
        const headers = { 'Content-Type': 'application/json' };
        const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
        if (token) headers['RequestVerificationToken'] = token;
        return headers;
    }

    async _handleResponse(response) {
        if (!response.ok) {
            const errorData = await response.json().catch(() => ({ error: response.statusText }));
            throw Object.assign(new Error(errorData.error || 'Request failed'), { status: response.status, data: errorData });
        }
        const text = await response.text();
        return text ? JSON.parse(text) : null;
    }
}
