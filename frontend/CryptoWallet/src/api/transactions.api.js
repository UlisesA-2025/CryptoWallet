import { apiFetch } from "./http";

export function getTransactions() {
    return apiFetch("/transactions");
}

export function createTransaction(payload) {
    return apiFetch("/transactions", {
        method: "POST",
        body: JSON.stringify(payload),
    });
}

export function pathTransaction(id, payload) {
    return apiFetch(`/transactions/${id}`, {
        method: "PATCH",
        body: JSON.stringify(payload),
    });
}

export function deleteTransaction(id) {
    return apiFetch(`/transactions/${id}`, {
        method: "DELETE",
    });
}