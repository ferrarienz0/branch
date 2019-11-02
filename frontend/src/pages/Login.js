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
        warning: '',
        loading: false,
    };
    handleSubmit = async e => {
        e.preventDefault();
        this.setState({ loading: true });
        await api
            .post('/session', {
                Nickname: this.state.username,
                PasswordHash: this.state.password,
                ValidTime: 200,
            })
            .then(res => {
                this.setState({ token: res.data.Token, isSession: true });
            })
            .catch(err => {
                this.setState({ warning: 'Usuário ou senha incorretos' });
            });
        this.setState({ loading: false });
    };

    render() {
        if (this.state.isSession) {
            return <Redirect to={`/home/${this.state.token}/u/8`} />;
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
                    <p id="warning">{this.state.warning}</p>
                    <Link
                        id="login-button"
                        to={`/home/${this.state.password}`}
                        type="submit"
                        onClick={this.handleSubmit}
                    >
                        {this.state.loading ? (
                            <p>Carregando...</p>
                        ) : (
                            <p>Login</p>
                        )}
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
