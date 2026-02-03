import { apiFetch } from "./http";

export function getArsPrice(exchange, cryptoCode) {
    return apiFetch(`/prices/${exchange}/${cryptoCode}`);
}