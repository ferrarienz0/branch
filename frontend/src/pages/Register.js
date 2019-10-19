import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './Register.css';
import viacep from '../services/viacep';
import logo from '../assets/logo.svg';
const axios = require('axios');

export default class Register extends Component {
    state = {
        name: '',
        lastname: '',
        CEP: '',
        logradouro: '',
        complemento: '',
        bairro: '',
        cidade: '',
        estado: '',
        datebirth: '',
        email: '',
        username: '',
        password: '',
        next: 0,
    };
    handleSubmit = e => {
        e.preventDefault();
        console.log(this.state);
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
    handleLastname = e => {
        this.setState({
            lastname: e.target.value,
        });
    };
    handleName = e => {
        this.setState({
            lastname: e.target.value,
        });
    };
    render() {
        const { next } = this.state;
        switch (next) {
            case 0:
                return (
                    <div id="register-container">
                        <form id="form" onSubmit={this.handleSubmit}>
                            <img id="logo" src={logo} alt="Branch" />
                            <input
                                id="name-input"
                                placeholder="Nome"
                                onChange={this.handleName}
                            />
                            <input
                                id="lastname-input"
                                placeholder="Sobrenome"
                                onChange={this.handleLastname}
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
                                        datebirth: new Date(e.target.value),
                                    })
                                }
                            />
                            <button
                                id="next-button"
                                className="orange"
                                onClick={e =>
                                    this.setState({
                                        next: 1,
                                    })
                                }
                            >
                                Próximo
                            </button>
                        </form>
                    </div>
                );
            case 1:
                return (
                    <div id="register-container">
                        <form id="form" onSubmit={this.handleSubmit}>
                            <img id="logo" src={logo} alt="Branch" />
                            <input
                                id="email-input"
                                placeholder="E-mail"
                                onChange={e =>
                                    this.setState({
                                        email: e.target.value,
                                    })
                                }
                            />
                            <input
                                id="user-input"
                                placeholder="Nome de usuário"
                                onChange={e =>
                                    this.setState({
                                        email: e.target.value,
                                    })
                                }
                            />
                            <input
                                id="password-input"
                                placeholder="Senha"
                                onChange={e =>
                                    this.setState({
                                        email: e.target.value,
                                    })
                                }
                            />
                            <input
                                id="pass"
                                placeholder="Confirme a senha"
                                onChange={e =>
                                    this.setState({
                                        email: e.target.value,
                                    })
                                }
                            />
                            <div className="row">
                                <button
                                    id="back-button"
                                    onClick={e =>
                                        this.setState({
                                            next: 0,
                                        })
                                    }
                                >
                                    Voltar
                                </button>
                                <Link
                                    id="register-button"
                                    to="/wait"
                                    type="submit"
                                    onSubmit={this.handleSubmit}
                                >
                                    Registrar
                                </Link>
                            </div>
                        </form>
                    </div>
                );
            default:
                break;
        }
    }
}
