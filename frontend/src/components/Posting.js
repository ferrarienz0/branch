import React, { Component } from 'react';
import { FaPaperPlane, FaPaperclip } from 'react-icons/fa';
import './Posting.css';
import api from '../services/api';

export default class Posting extends Component {
    state = {
        image: { id: -1, URL: '' },
        temp: '',
        text: '',
        preview: '',
    };

    handlePost = async () => {
        const { image, text, temp } = this.state;
        const { token, onClose } = this.props;
        console.log(temp);
        if (Boolean(temp.name)) {
            let config = {
                headers: {
                    Accept: '',
                    'Content-Type': 'multipart/form-data',
                },
            };
            let formData = new FormData();
            formData.append('image', temp);
            const { data } = await api.post(
                `/media?AccessToken=${token}&IsUserMedia=false`,
                formData,
                config
            );
            this.setState({ image: { id: data[0].Id, URL: data[0].URL } });
        }
        const { data: postagem } = await api.post(
            `/post?AccessToken=${token}`,
            {
                Text: text,
                Medias: image.id === -1 ? [] : [image.id],
            }
        );
        console.log(postagem);
        onClose();
    };

    handleFile = e => {
        console.log(e.target.files[0]);
        this.setState({
            preview: URL.createObjectURL(e.target.files[0]),
            temp: e.target.files[0],
        });
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
