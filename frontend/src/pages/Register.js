import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './Start.css';
import api from '../services/api';
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
        console.log(this.state.datebirth);
    };
    async handleAddress(e){
        const address = await axios.get(`viacep.com.br/ws/${e.target.value}/json/`)
        this.setState({CEP: e.target.value,
                       logradouro: address.logradouro,
                       complemento: address.complemento,
                       bairro: address.bairro,
                       cidade: address.localidade,
                       estado: address.uf});
    }
    render() {
        const { next } = this.state;
        switch (next) {
            case 0:
                return (
                    <div className="start-container">
                        <form onSubmit={this.handleSubmit}>
                            <img src={logo} alt="Branch" />
                            <input
                                placeholder="Nome"
                                onChange={e =>
                                    this.setState({ name: e.target.value })
                                }
                            />
                            <input
                                placeholder="Sobrenome"
                                onChange={e =>
                                    this.setState({
                                        lastname: e.target.value,
                                    })
                                }
                            />
                            <input
                                placeholder="CEP"
                                type="CEP"
                                onChange={e => this.handleAddress(e)}
                            />
                            <input
                                min="1900-01-01"
                                type="date"
                                onChange={e =>
                                    this.setState({
                                        datebirth: new Date(e.target.value),
                                    })
                                }
                            />
                            <button
                                id="proximo"
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
                    <div className="start-container">
                        <form onSubmit={this.handleSubmit}>
                            <img src={logo} alt="Branch" />
                            <input
                                placeholder="E-mail"
                                onChange={e =>
                                    this.setState({
                                        email: e.target.value,
                                    })
                                }
                            />
                            <input
                                placeholder="Nome de usuário"
                                onChange={e =>
                                    this.setState({
                                        email: e.target.value,
                                    })
                                }
                            />
                            <input
                                placeholder="Senha"
                                onChange={e =>
                                    this.setState({
                                        email: e.target.value,
                                    })
                                }
                            />
                            <input
                                placeholder="Confirme a senha"
                                onChange={e =>
                                    this.setState({
                                        email: e.target.value,
                                    })
                                }
                            />
                            <div className="row">
                                <button
                                    className="orange"
                                    onClick={e =>
                                        this.setState({
                                            next: 0,
                                        })
                                    }
                                >
                                    Voltar
                                </button>
                                <Link className="red" to="/wait" type="submit">
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
