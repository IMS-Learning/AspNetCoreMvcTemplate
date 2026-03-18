/**
 * Reusable modal module.
 */
export class Modal {
    constructor(element) {
        this.element = typeof element === 'string'
            ? document.querySelector(element)
            : element;
        this._bsModal = window.bootstrap?.Modal.getOrCreateInstance(this.element);
    }

    show() {
        this._bsModal?.show();
    }

    hide() {
        this._bsModal?.hide();
    }
}
