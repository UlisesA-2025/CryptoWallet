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

export function deleteTransaction(id) {
    return apiFetch(`/transactions/${id}`, {
        method: "DELETE",
    });
}