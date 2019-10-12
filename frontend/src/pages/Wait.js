import React, { Component } from 'react';
import { Link } from 'react-router-dom';
//import './Start.css';
//import api from '../services/api';
import logo from '../assets/logo.svg';

export default class Wait extends Component {
    state = {};
    handleSubmit = e => {
        e.preventDefault();
    };
    render() {
        return (
            <div className="start-container">
                <h1>Hello</h1>
            </div>
        );
    }
}
