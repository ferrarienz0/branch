import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import './Login.css';
import md5 from 'md5';
import api from '../services/api';
import logo from '../assets/logo.svg';

export default class Login extends Component {
    state = {
        username: '',
        password: '',
        isSession: false,
        token: '',
    };
    handleSubmit = async e => {
        e.preventDefault();
        let { data: token } = await api.post('/session', {
            Nickname: this.state.username,
            PasswordHash: this.state.password,
            ValidTime: 200,
        });
        this.setState({ token: token.Token, isSession: true });
        console.log(token.Token);
    };

    render() {
        if (this.state.isSession) {
            return <Redirect to={`/home/${this.state.token}`} />;
        }
        return (
            <div id="login-container">
                <form id="form">
                    <img id="logo" src={logo} alt="Branch" />
                    <input
                        id="login-input"
                        placeholder="Nome de usuário"
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
                        onClick={this.handleSubmit}
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
