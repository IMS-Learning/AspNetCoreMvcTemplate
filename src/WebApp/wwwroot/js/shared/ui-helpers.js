/**
 * UI utility helpers (modals, toasts, loaders).
 */

import { showToast } from './modules/toast.js';
import { showLoader, hideLoader } from './modules/loader.js';

export { showToast, showLoader, hideLoader };

/**
 * Shows a confirmation dialog and returns a promise.
 * @param {string} message
 * @returns {Promise<boolean>}
 */
export function confirm(message) {
    return new Promise(resolve => {
        resolve(window.confirm(message));
    });
}

/**
 * Smoothly scrolls to an element.
 * @param {string|HTMLElement} target
 */
export function scrollTo(target) {
    const element = typeof target === 'string' ? document.querySelector(target) : target;
    element?.scrollIntoView({ behavior: 'smooth' });
}
