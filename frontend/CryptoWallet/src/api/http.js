const API_BASE_URL = "https://localhost:7047";

export async function apiFetch(path, options = {}) {
    const url = `${API_BASE_URL}${path}`;

    const res = await fetch(url, {
        ...options,
        headers:{
            "Content-Type": "application/json",
            ...(options.headers || {}),
        },
    });

    if (!res.ok) {
        const text = await res.text();
        throw new Error(text || `HTTP ${res.status}`);
    }

    if(res.status === 204) return null;

    return res.json();

}