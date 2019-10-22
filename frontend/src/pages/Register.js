import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import md5 from 'md5';
import './Register.css';
import viacep from '../services/viacep';
import api from '../services/api';
import logo from '../assets/logo.svg';

export default class Register extends Component {
    state = {
        user: {
            Firstname: '',
            Lastname: '',
            Nickname: '',
            Password: '',
            Email: '',
            BirthDate: '',
        },
        CEP: '',
        Logradouro: '',
        complemento: '',
        bairro: '',
        cidade: '',
        estado: '',
        token: '',
        isSession: false,
    };
    handleSubmit = async e => {
        e.preventDefault();
        console.log(this.state.user);
        await api.post('/user', this.state.user);
        console.log(this.state.user);
        const sessionobj = {
            Nickname: this.state.user.Nickname,
            PasswordHash: this.state.user.Password,
            ValidTime: 200,
        };
        console.log(sessionobj);
        let { data: token } = await api.post('/session', sessionobj);
        this.setState({ token: token.Token, isSession: true });
        console.log(token.Token);
    };
    handleAddress = async e => {
        const { data: address } = await viacep.get(
            `/ws/${e.target.value}/json/`
        );
        console.log(address);
        this.setState({
            CEP: address.cep,
            logradouro: address.logradouro,
            complemento: address.complemento,
            bairro: address.bairro,
            cidade: address.localidade,
            estado: address.uf,
        });
    };
    handlePassword = e => {
        this.setState({
            user: {
                ...this.state.user,
                Password: md5(e.target.value),
            },
        });
    };
    render() {
        if (this.state.isSession) {
            return <Redirect to={`/home/${this.state.token}`} />;
        }
        return (
            <div id="register-container">
                <form id="form">
                    <img id="logo" src={logo} alt="Branch" />
                    <input
                        id="name-input"
                        placeholder="Nome"
                        onChange={e => {
                            this.setState({
                                user: {
                                    ...this.state.user,
                                    Firstname: encodeURIComponent(
                                        e.target.value
                                    ),
                                },
                            });
                        }}
                    />
                    <input
                        id="lastname-input"
                        placeholder="Sobrenome"
                        onChange={e =>
                            this.setState({
                                user: {
                                    ...this.state.user,
                                    Lastname: encodeURIComponent(
                                        e.target.value
                                    ),
                                },
                            })
                        }
                    />
                    <input
                        id="cep-input"
                        placeholder="CEP"
                        type="CEP"
                        onChange={this.handleAddress}
                    />
                    <input
                        id="date-input"
                        min="1900-01-01"
                        type="date"
                        onChange={e =>
                            this.setState({
                                user: {
                                    ...this.state.user,
                                    BirthDate: e.target.value,
                                },
                            })
                        }
                    />
                    <input
                        id="email-input"
                        placeholder="E-mail"
                        onChange={e =>
                            this.setState({
                                user: {
                                    ...this.state.user,
                                    Email: e.target.value,
                                },
                            })
                        }
                    />
                    <input
                        id="user-input"
                        placeholder="Nome de usuário"
                        onChange={e =>
                            this.setState({
                                user: {
                                    ...this.state.user,
                                    Nickname: encodeURIComponent(
                                        e.target.value
                                    ),
                                },
                            })
                        }
                    />
                    <input
                        id="password-input"
                        placeholder="Senha"
                        type="password"
                        onChange={this.handlePassword}
                    />
                    <Link
                        id="register-button"
                        to={`/home/${this.state.token}`}
                        onClick={this.handleSubmit}
                    >
                        Registrar
                    </Link>
                </form>
            </div>
        );
    }
}
