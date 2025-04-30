// for local back url: http://localhost:5262/api
// render server : "https://digital-menu-2-81mx.onrender.com/api"

import def from 'ajv/dist/vocabularies/discriminator';
import axios from 'axios';

const api = axios.create({
    baseURL: "https://digital-menu-2-81mx.onrender.com/api",
    headers: {
        'Content-Type': 'application/json',
    },
});

api.interceptors.request.use(config => {
    const token = localStorage.getItem("token");
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api;