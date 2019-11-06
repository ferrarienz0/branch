import React, { Component } from 'react';
import { FaPaperPlane, FaPaperclip } from 'react-icons/fa';
import './Posting.css';
import api from '../services/api';

export default class Posting extends Component {
    state = {
        image: { id: -1, URL: '' },
        text: '',
        preview: '',
    };
    handlePost = async () => {
        const { image, text, preview } = this.state;
        const { token, onClose } = this.props;
        let formData = new FormData();
        formData.append('image', preview);
        let config = {
            headers: {
                Accept: '',
                'Content-Type': 'multipart/form-data',
            },
        };
        const { data } = await api.post(
            `/media?AccessToken=${token}&IsUserMedia=false`,
            formData,
            config
        );
        console.log(data);
        this.setState({ image: { id: data[0].Id, URL: data[0].URL } });
        await api.post(`/post?AccessToken=${token}`, {
            Text: text,
            Medias: image.id === -1 ? [] : [image.id],
        });
        onClose();
    };

    handleFile = e => {
        this.setState({ preview: URL.createObjectURL(e.target.files[0]) });
    };

    render() {
        const { preview } = this.state;
        return (
            <div id="posting-container">
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
                <img id="preview" src={preview} alt="" />
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
