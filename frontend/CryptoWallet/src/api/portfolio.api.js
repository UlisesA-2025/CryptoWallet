import { apiFetch } from "./http";

export function getPortfolio() {
    return apiFetch("/portfolio");
}