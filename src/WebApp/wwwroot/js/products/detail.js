/**
 * Product detail page interactions.
 */
export function initProductDetail() {
    const addToCartBtn = document.getElementById('addToCart');
    if (addToCartBtn) {
        addToCartBtn.addEventListener('click', () => {
            const productId = addToCartBtn.dataset.productId;
            const quantity = document.getElementById('quantity')?.value ?? 1;
            console.info(`Adding product ${productId} (qty: ${quantity}) to cart`);
        });
    }
}
