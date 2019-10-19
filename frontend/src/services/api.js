import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:44377',
});

export default api;
