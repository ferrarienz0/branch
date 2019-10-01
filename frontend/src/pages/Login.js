import React, { useState } from 'react';
import './Login.css';
//import api from '../services/api';
import logo from '../assets/logo.svg';

export default function Login({ history }) {
    //const [ username, setUsername ] = useState('');
    async function handleSubmit(e) {
        e.preventDefault();
        //const response = await api.post('/algumacoisa');
        history.push('/home');
    }
    return (
        <div className="login-container">
            <form onSubmit={handleSubmit}>
                <img src={logo} alt="Branch"/>
                <input
                    placeholder="Nome de usuário ou e-mail"
                    //value={username}
                    //onChange={e => setUsername(e.target.value)}
                />
                <input type="password" placeholder="Senha"/>
                <button className="login" type="submit">Login</button>
                <h1>Ainda não possui uma conta?</h1>
                <button className="registrese" type="submit">Registre-se</button>
            </form>
            <h1 className="ou">ou</h1>
            <button className="halfpugg">Entrar com HalfPugg</button>
        </div>
    );
}