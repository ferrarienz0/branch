import React, { Component } from 'react';
import { IoIosPerson } from 'react-icons/io';
import './Home.css';
import icone from '../assets/icone.svg';

export default class Home extends Component {
    render() {
        return (
            <div className="home-container">
                <link
                    href="https://fonts.googleapis.com/icon?family=Material+Icons"
                    rel="stylesheet"
                />
                <div className="head">
                    <img src={icone} alt="Branch" />
                </div>
                <div className="body">
                    <div className="perfil">
                        <div className="photo">
                            <IoIosPerson />
                        </div>
                    </div>
                    <div className="feed"></div>
                </div>
            </div>
        );
    }
}
