import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './Login.css';
import md5 from 'md5';
//import api from '../services/api';
import logo from '../assets/logo.svg';

export default class Login extends Component {
    state = {
        username: '',
        password: '',
    };
    handleSubmit = e => {
        e.preventDefault();
        console.log(this.state.username);
        //const response = await api.post('/algumacoisa');
    };

    render() {
        return (
            <div id="login-container">
                <form id="form" onSubmit={this.handleSubmit}>
                    <img id="logo" src={logo} alt="Branch" />
                    <input
                        id="login-input"
                        placeholder="Nome de usuário ou e-mail"
                        onChange={e =>
                            this.setState({ username: e.target.value })
                        }
                    />
                    <input
                        id="password-input"
                        type="password"
                        placeholder="Senha"
                        onChange={e =>
                            this.setState({ password: md5(e.target.value) })
                        }
                    />
                    <Link
                        id="login-button"
                        to={`/home/${this.state.password}`}
                        type="submit"
                    >
                        Login
                    </Link>
                    <h1 id="phrase">Ainda não possui uma conta?</h1>
                    <Link id="register-button" to="/register" type="submit">
                        Registre-se
                    </Link>
                </form>
                <h1 id="ou">ou</h1>
                <button id="halfpugg-button">Entrar com HalfPugg</button>
            </div>
        );
    }
}
