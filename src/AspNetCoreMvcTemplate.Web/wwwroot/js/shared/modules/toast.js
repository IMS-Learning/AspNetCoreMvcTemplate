/**
 * Reusable toast notification module.
 */

let _toastContainer = null;

function getContainer() {
    if (!_toastContainer) {
        _toastContainer = document.createElement('div');
        _toastContainer.className = 'toast-container position-fixed bottom-0 end-0 p-3';
        _toastContainer.style.zIndex = '1100';
        document.body.appendChild(_toastContainer);
    }
    return _toastContainer;
}

/**
 * Shows a toast notification.
 * @param {string} message
 * @param {'success'|'danger'|'warning'|'info'} type
 * @param {number} duration
 */
export function showToast(message, type = 'info', duration = 4000) {
    const container = getContainer();
    const toast = document.createElement('div');
    toast.className = `toast align-items-center text-bg-${type} border-0`;
    toast.setAttribute('role', 'alert');
    toast.innerHTML = `
        <div class="d-flex">
            <div class="toast-body">${message}</div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
        </div>`;
    container.appendChild(toast);

    const bsToast = window.bootstrap?.Toast.getOrCreateInstance(toast, { delay: duration });
    bsToast?.show();
    toast.addEventListener('hidden.bs.toast', () => toast.remove());
}
