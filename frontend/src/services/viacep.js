import axios from 'axios';

const viacep = axios.create({
    baseURL: 'http://viacep.com.br',
});

export default viacep;
