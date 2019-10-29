import React, { Component } from 'react';
import { IoMdPerson, IoMdMenu } from 'react-icons/io';
import { FaPaperPlane, FaPaperclip, FaArrowLeft } from 'react-icons/fa';
import './Posting.css';
import api from '../services/api';

export default class Posting extends Component {
    state = {
        image: null,
        type: this.props.type,
        text: '',
    };
    handlePost = async () => {
        await api
            .post(`/post?AccessToken=${this.props.token}`, {
                text: this.props.user + ' ' + this.state.text,
            })
            .then(res => {
                this.setState({ type: '' });
                console.log(res.data);
            });
    };
    render() {
        return (
            <div id="posting-container">
                <div id="head">
                    <FaArrowLeft id="back-icon" onClick={this.props.onClose} />
                </div>
                <textarea
                    id="text-area"
                    type="text"
                    onChange={e => this.setState({ text: e.target.value })}
                />
                <div id="options-post">
                    <button id="send" onClick={this.handlePost}>
                        <FaPaperPlane id="send-icon" />
                    </button>
                    <button id="media">
                        <FaPaperclip id="clip-icon" />
                    </button>
                </div>
                <input id="media" type="file" name="pic" accept=".png"></input>
            </div>
        );
    }
}
