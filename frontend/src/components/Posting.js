import React, { Component } from 'react';
import { FaPaperPlane, FaPaperclip } from 'react-icons/fa';
import './Posting.css';
import api from '../services/api';

export default class Posting extends Component {
    state = {
        image: { id: -1, URL: '' },
        text: '',
        temp: '',
    };
    handlePost = async () => {
        let formData = new FormData();
        formData.append('image', this.state.temp);
        const { data } = await api.post(
            `/media?AccessToken=${this.props.token}&IsUserMedia=false`,
            formData,
            config
        );
        this.setState({ image: { id: data[0].Id, URL: data[0].URL } });
        await api.post(`/post?AccessToken=${this.props.token}`, {
            Text: this.state.text,
            Medias: this.state.image.id === -1 ? [] : [this.state.image.id],
        });
        let config = {
            headers: {
                Accept: '',
                'Content-Type': 'multipart/form-data',
            },
        };
        this.props.onClose();
    };
    handleFile = async e => {
        console.log(e.target.files[0]);
        this.setState({ temp: e.target.files[0] });
    };
    render() {
        return (
            <div id="posting-container">
                {/*<div id="head">
                    <FaArrowLeft id="back-icon" onClick={this.props.onClose} />
        </div>*/}
                <textarea
                    id="text-area"
                    type="text"
                    onChange={e => this.setState({ text: e.target.value })}
                />
                <div id="buttons">
                    <label id="media" htmlFor="selecao-arquivo">
                        <FaPaperclip id="clip-icon" />
                    </label>
                    <button id="send" onClick={this.handlePost}>
                        <FaPaperPlane id="send-icon" />
                    </button>
                </div>
                <img
                    id="preview"
                    src={this.state.image === null ? '' : this.state.image.URL}
                    alt=""
                />
                <input
                    id="selecao-arquivo"
                    type="file"
                    name="pic"
                    onChange={this.handleFile}
                ></input>
            </div>
        );
    }
}
