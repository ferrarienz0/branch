import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import md5 from 'md5';
import { Container, Submit, Calendar } from './styles.js';
import viacep from '../../services/viacep';
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
        address: {
            CEP: '',
            Logradouro: '',
            complemento: '',
            bairro: '',
            cidade: '',
            estado: '',
        },
        token: '',
        warning: '',
        redirect: false,
    };

    handleSubmit = e => {
        const { user } = this.state;
        e.preventDefault();
        api.post('/user', user)
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

    handleAddress = async e => {
        const { data: address } = await viacep.get(
            `/ws/${e.target.value}/json/`
        );
        this.setState({
            address: {
                CEP: address.cep,
                logradouro: address.logradouro,
                complemento: address.complemento,
                bairro: address.bairro,
                cidade: address.localidade,
                estado: address.uf,
            },
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
                                    name: encodeURIComponent(e.target.value),
                                    ...user,
                                },
                            });
                        }}
                    />
                    <input
                        placeholder="Sobrenome"
                        onChange={e =>
                            this.setState({
                                user: {
                                    lastName: encodeURIComponent(
                                        e.target.value
                                    ),
                                    ...user,
                                },
                            })
                        }
                    />
                    <input
                        placeholder="CEP"
                        type="CEP"
                        onChange={this.handleAddress}
                    />
                    <Calendar
                        min="1900-01-01"
                        max={new Date().toISOString().split('T')[0]}
                        type="date"
                        required="required"
                        onChange={e =>
                            this.setState({
                                user: {
                                    birthDate: e.target.value,
                                    ...user,
                                },
                            })
                        }
                    />
                    <input
                        placeholder="E-mail"
                        onChange={e =>
                            this.setState({
                                user: {
                                    email: e.target.value,
                                    ...user,
                                },
                            })
                        }
                    />
                    <input
                        placeholder="Nome de usuário"
                        onChange={e =>
                            this.setState({
                                user: {
                                    userName: encodeURIComponent(
                                        e.target.value
                                    ),
                                    ...user,
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
                                    password: md5(e.target.value),
                                    ...user,
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
