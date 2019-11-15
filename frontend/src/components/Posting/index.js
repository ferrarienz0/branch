import React, { Component } from 'react';
import { FaPaperPlane, FaPaperclip } from 'react-icons/fa';
import { Container } from './styles';
import api from '../../services/api';

export default class Posting extends Component {
    state = {
        data: [],
        temp: '',
        text: '',
        preview: '',
    };

    handlePost = async () => {
        const { data, text, temp } = this.state;
        const { token, parent, onClose } = this.props;
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
                `/media/create?AccessToken=${token}&IsUserMedia=false`,
                formData,
                config
            );
            this.setState({ data });
        }
        console.log(parent);
        if (parent === undefined) {
            await api.post(`/post/create?AccessToken=${token}`, {
                Text: text,
                Medias: data[0] === undefined ? [] : [data[0].Id],
            });
        } else {
            await api.post(`/post/create?AccessToken=${token}`, {
                Parent: parent,
                Text: text,
                Medias: data[0] === undefined ? [] : [data[0].Id],
            });
        }
        onClose();
    };

    handleFile = e => {
        this.setState({
            preview: URL.createObjectURL(e.target.files[0]),
            temp: e.target.files[0],
        });
    };

    render() {
        const { preview } = this.state;
        return (
            <Container>
                <textarea
                    id="text-area"
                    type="text"
                    onChange={e => this.setState({ text: e.target.value })}
                />
                <div id="buttons">
                    <label id="media" htmlFor="selecao-arquivo">
                        <FaPaperclip id="media-icon" />
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
            </Container>
        );
    }
}
