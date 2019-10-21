import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import md5 from 'md5';
import './Register.css';
import viacep from '../services/viacep';
import logo from '../assets/logo.svg';
const axios = require('axios');

export default class Register extends Component {
    state = {
        Firstname: '',
        Lastname: '',
        Nickname: '',
        Password: '',
        Email: '',
        CEP: '',
        logradouro: '',
        complemento: '',
        bairro: '',
        cidade: '',
        estado: '',
        datebirth: '',
        next: 0,
        pass: null,
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
    handleFirstname = e => {
        this.setState({
            Firstname: e.target.value,
        });
    };
    handleLastname = e => {
        this.setState({
            Lastname: e.target.value,
        });
    };
    handleBirthDate = e => {
        this.setState({
            datebirth: new Date(e.target.value),
        });
    };
    handleEmail = e => {
        this.setState({
            Email: e.target.value,
        });
    };
    handleNickname = e => {
        this.setState({
            Nickname: e.target.value,
        });
    };
    handlePassword = e => {
        if (this.state.pass === null) this.setState({ pass: e.target.value });
        else if (this.state.pass === e.target.value) {
            this.setState({
                Password: md5(this.state.pass),
            });
        } else this.setState({ pass: null });
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
                                onChange={this.handleFirstname}
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
                                onChange={this.handleBirthDate}
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
                                onChange={this.handleEmail}
                            />
                            <input
                                id="user-input"
                                placeholder="Nome de usuário"
                                onChange={this.handleNickname}
                            />
                            <input
                                id="password-input"
                                placeholder="Senha"
                                onChange={this.handlePassword}
                            />
                            <input
                                id="pass"
                                placeholder="Confirme a senha"
                                onChange={this.handlePassword}
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
