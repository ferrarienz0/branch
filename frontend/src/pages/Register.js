import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './Register.css';
//import api from '../services/api';
import logo from '../assets/logo.svg';

export default class Register extends Component {
    state = {
        name: '',
        lastname: '',
        datebirth: {
            day: 1,
            mounth: 1,
            year: 1,
        },
        email: '',
        username: '',
        password: '',
        next: 0,
        adfasc: <div></div>,
    };
    handleSubmit = e => {
        e.preventDefault();
        console.log(this.state.username);
    };
    render() {
        const { next } = this.state;
        switch (next) {
            case 0:
                return (
                    <div className="register-container">
                        <form onSubmit={this.handleSubmit}>
                            <img src={logo} alt="Branch" />
                            <div id="allname">
                                <input
                                    id="name"
                                    placeholder="Nome"
                                    onChange={e =>
                                        this.setState({ name: e.target.value })
                                    }
                                />
                                <input
                                    id="lastname"
                                    placeholder="Sobrenome"
                                    onChange={e =>
                                        this.setState({
                                            lastname: e.target.value,
                                        })
                                    }
                                />
                            </div>
                            <input
                                id="date"
                                min="1900-01-01"
                                type="date"
                                onChange={e =>
                                    this.setState({
                                        datebirth: {
                                            day: e.day,
                                            mounth: e.mounth,
                                            year: e.year,
                                        },
                                    })
                                }
                            />
                            <button
                                id="proximo"
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
                    <div className="register-container">
                        <div>hello</div>
                    </div>
                );
            default:
                break;
        }
    }
}
