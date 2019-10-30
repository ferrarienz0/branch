import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import md5 from 'md5';
import './Register.css';
import viacep from '../services/viacep';
import api from '../services/api';
import logo from '../assets/logo.svg';
import UserImage from '../components/UserImage';

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
        warning: '',
        image: null,
        isSession: false,
    };
    handleSubmit = async e => {
        e.preventDefault();
        await api
            .post('/user', this.state.user)
            .then(async () => {
                let { data: token } = await api.post('/session', {
                    Nickname: this.state.user.Nickname,
                    PasswordHash: this.state.user.Password,
                    ValidTime: 200,
                });
                this.setState({ token: token.Token, isSession: true });
            })
            .catch(() => {
                this.setState({ warning: 'Ocorreu um erro' });
            });
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
    handleFile = async e => {
        let config = {
            headers: {
                Accept: '',
                'Content-Type': 'multipart/form-data',
            },
        };
        let formData = new FormData();
        formData.append('image', e.target.files[0]);
        const { data } = await api.post(
            `/media?AccessToken=${this.props.token}&IsUserMedia=true`,
            formData,
            config
        );
        this.setState({ image: data[0].URL });
    };
    render() {
        console.log(this.state.image);
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
                        max={new Date().toISOString().split('T')[0]}
                        type="date"
                        required="required"
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
                    <label id="media" htmlFor="selecao-arquivo">
                        <UserImage
                            htmlFor="selecao-arquivo"
                            id="user-image"
                            size="100px"
                            image={this.state.image}
                        />
                    </label>
                    <input
                        id="selecao-arquivo"
                        type="file"
                        name="pic"
                        onChange={this.handleFile}
                    ></input>
                    <p id="warning">{this.state.warning}</p>
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
