import React, { Component } from 'react';
import { FaPaperPlane, FaPaperclip, FaArrowLeft } from 'react-icons/fa';
import './Posting.css';
import api from '../services/api';

export default class Posting extends Component {
    state = {
        image: { id: -1, URL: '' },
        text: '',
    };
    handlePost = async () => {
        await api.post(`/post?AccessToken=${this.props.token}`, {
            Text: this.state.text,
            Medias: this.state.image.id === -1 ? [] : [this.state.image.id],
        });
        this.props.onClose();
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
            `/media?AccessToken=${this.props.token}&IsUserMedia=false`,
            formData,
            config
        );
        this.setState({ image: { id: data[0].Id, URL: data[0].URL } });
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
                    <div id="buttons">
                        <label id="media" htmlFor="selecao-arquivo">
                            <FaPaperclip id="clip-icon" />
                        </label>
                        <button id="send" onClick={this.handlePost}>
                            <FaPaperPlane id="send-icon" />
                        </button>
                    </div>
                    <div id="not-flex">
                        <img
                            id="preview"
                            src={
                                this.state.image === null
                                    ? ''
                                    : this.state.image.URL
                            }
                            alt=""
                        />
                    </div>
                </div>
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
