import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Container, LogIn, Register, NotYet } from './styles.js';
import md5 from 'md5';
import api from '../../services/api';
import logo from '../../assets/logo.svg';

export default class Login extends Component {
    state = {
        userName: '',
        password: '',
        redirect: false,
        token: '',
        warning: '',
        loading: false,
    };

    handleSubmit = e => {
        const { userName, password } = this.state;
        this.setState({ loading: true });
        e.preventDefault();
        api.post('/session', {
            Nickname: userName,
            PasswordHash: password,
            ValidTime: 200,
        })
            .then(res => {
                this.setState({ token: res.data.Token, redirect: true });
            })
            .catch(() => {
                this.setState({ warning: 'Usuário ou senha incorretos' });
            });
        this.setState({ loading: false });
    };

    render() {
        const { token, warning, redirect, loading } = this.state;
        if (redirect) return <Redirect to={`/home/${token}`} />;
        return (
            <Container>
                <form>
                    <img src={logo} alt="Branch" />
                    <input
                        placeholder="Nome de usuário"
                        onChange={e =>
                            this.setState({ userName: e.target.value })
                        }
                    />
                    <input
                        type="password"
                        placeholder="Senha"
                        onChange={e =>
                            this.setState({ password: md5(e.target.value) })
                        }
                    />
                    <p>{warning}</p>
                    <LogIn type="submit" onClick={this.handleSubmit}>
                        {loading ? <p>...</p> : <p>Login</p>}
                    </LogIn>
                    <NotYet>Ainda não possui uma conta?</NotYet>
                    <Register to="/register" type="submit">
                        Registre-se
                    </Register>
                </form>
            </Container>
        );
    }
}
