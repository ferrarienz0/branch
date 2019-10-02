import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './Login.css';
//import api from '../services/api';
import logo from '../assets/logo.svg';

export default class Login extends Component {
    //export default function Login({ history }) {
    //const [ username, setUsername ] = useState('');
    state = {
        username: '',
        password: '',
    };
    //const [eUsername, eSetUsername] = useState();
    //const [ePassword, eSetPassword] = useState();
    handleSubmit = e => {
        e.preventDefault();
        console.log(this.state.username);
        //const response = await api.post('/algumacoisa');
        //history.push('/home');
    };

    render() {
        return (
            <div className="login-container">
                <form onSubmit={this.handleSubmit}>
                    <img src={logo} alt="Branch" />
                    <input
                        placeholder="Nome de usuário ou e-mail"
                        onChange={e =>
                            this.setState({ username: e.target.value })
                        }
                    />
                    <input
                        type="password"
                        placeholder="Senha"
                        onChange={e =>
                            this.setState({ password: e.target.value })
                        }
                    />
                    <Link id="login" to="/home" type="submit">
                        Login
                    </Link>
                    <h1>Ainda não possui uma conta?</h1>
                    <Link id="registrese" to="/register" type="submit">
                        Registre-se
                    </Link>
                </form>
                <h1 className="ou">ou</h1>
                <button className="halfpugg">Entrar com HalfPugg</button>
            </div>
        );
    }
}
