import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import md5 from 'md5';
import { Container, Submit, Calendar } from './styles.js';
import api from '../../services/api';
import logo from '../../assets/logo.svg';

export default class Register extends Component {
    state = {
        user: {
            name: '',
            lastName: '',
            userName: '',
            password: '',
            email: '',
            birthDate: '',
        },
        token: '',
        warning: '',
        redirect: false,
    };

    handleSubmit = e => {
        const { user } = this.state;
        e.preventDefault();
        api.post('/user/create', {
            Firstname: user.name,
            Lastname: user.lastName,
            Nickname: user.userName,
            Password: user.password,
            Email: user.email,
            BirthDate: user.birthDate,
            IsPro: false,
        })
            .then(async () => {
                const { data: token } = await api.post('/session', {
                    NickName: user.userName,
                    PasswordHash: user.password,
                    ValidTime: 200,
                });
                this.setState({ token: token.Token, redirect: true });
            })
            .catch(() => {
                this.setState({ warning: 'Ocorreu um erro' });
            });
    };

    render() {
        const { user, token, warning, redirect } = this.state;
        if (redirect) return <Redirect to={`/home/${token}`} />;
        return (
            <Container>
                <form id="form">
                    <img id="logo" src={logo} alt="Branch" />
                    <input
                        placeholder="Nome"
                        onChange={e => {
                            this.setState({
                                user: {
                                    ...user,
                                    name: encodeURIComponent(e.target.value),
                                },
                            });
                        }}
                    />
                    <input
                        placeholder="Sobrenome"
                        onChange={e =>
                            this.setState({
                                user: {
                                    ...user,
                                    lastName: encodeURIComponent(
                                        e.target.value
                                    ),
                                },
                            })
                        }
                    />
                    <Calendar
                        min="1900-01-01"
                        max={new Date().toISOString().split('T')[0]}
                        type="date"
                        required="required"
                        onChange={e =>
                            this.setState({
                                user: {
                                    ...user,
                                    birthDate: e.target.value,
                                },
                            })
                        }
                    />
                    <input
                        placeholder="E-mail"
                        onChange={e =>
                            this.setState({
                                user: {
                                    ...user,
                                    email: e.target.value,
                                },
                            })
                        }
                    />
                    <input
                        placeholder="Nome de usuário"
                        onChange={e =>
                            this.setState({
                                user: {
                                    ...user,
                                    userName: encodeURIComponent(
                                        e.target.value
                                    ),
                                },
                            })
                        }
                    />
                    <input
                        placeholder="Senha"
                        type="password"
                        onChange={e =>
                            this.setState({
                                user: {
                                    ...user,
                                    password: md5(e.target.value),
                                },
                            })
                        }
                    />
                    <p id="warning">{warning}</p>
                    <Submit
                        id="register-button"
                        to={`/home/${token}`}
                        onClick={this.handleSubmit}
                    >
                        Registrar
                    </Submit>
                </form>
            </Container>
        );
    }
}
