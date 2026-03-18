/**
 * Reusable page loader/spinner module.
 */

let _loader = null;

function getLoader() {
    if (!_loader) {
        _loader = document.createElement('div');
        _loader.id = 'page-loader';
        _loader.className = 'd-none position-fixed top-0 start-0 w-100 h-100 d-flex align-items-center justify-content-center';
        _loader.style.backgroundColor = 'rgba(255,255,255,0.7)';
        _loader.style.zIndex = '9999';
        _loader.innerHTML = '<div class="spinner-border text-primary" role="status"><span class="visually-hidden">Loading...</span></div>';
        document.body.appendChild(_loader);
    }
    return _loader;
}

export function showLoader() {
    const loader = getLoader();
    loader.classList.remove('d-none');
    loader.classList.add('d-flex');
}

export function hideLoader() {
    const loader = getLoader();
    loader.classList.add('d-none');
    loader.classList.remove('d-flex');
}
