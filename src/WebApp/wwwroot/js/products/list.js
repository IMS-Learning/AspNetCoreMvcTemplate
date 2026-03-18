import { HttpClient } from '../shared/http-client.js';

const http = new HttpClient();

/**
 * Deletes a product by ID.
 * @param {number} id
 * @returns {Promise<void>}
 */
export async function deleteProduct(id) {
    await http.delete(`/api/products/${id}`);
}
