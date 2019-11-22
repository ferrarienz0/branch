import React, { Component } from 'react';
import { FaRegImage, FaPaperPlane } from 'react-icons/fa';
import { Container } from './styles';
import api from '../../services/api';

export default class CreateProduct extends Component {
    state = {
        name: '',
        description: '',
        price: 0.0,
        stock: 0,
        discount: 0.0,
        maxDiscount: 0.0,
        temp: '',
        preview: '',
    };

    handleName = e => {
        this.setState({ name: e.target.value.replace(/ /g, '_') });
    };

    handlePost = async e => {
        e.persist();
        const {
            name,
            description,
            price,
            stock,
            discount,
            maxDiscount,
            temp,
        } = this.state;
        const { token, onClose } = this.props;
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
        await api.post(`/product/create?AccessToken=${token}`, {
            Name: name,
            Description: description,
            Price: price,
            Stock: stock,
            CurrentDiscount: discount,
            MaxDiscount: maxDiscount,
            MediaId: data[0].Id,
        });
        await api.post(`/post/create?AccessToken=${token}`, {
            Text: '$' + name + ' :: ' + description,
            Medias: [data[0].Id],
        });
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
                <form>
                    <div id="aux">
                        <input
                            id="name"
                            placeholder="Nome do produto"
                            onChange={e => this.handleName(e)}
                        />
                        <input
                            id="price"
                            type="number"
                            placeholder="Preço"
                            onChange={e =>
                                this.setState({ price: e.target.value })
                            }
                        />
                        <input
                            id="stock"
                            type="number"
                            placeholder="Quantidade em estoque"
                            onChange={e =>
                                this.setState({ stock: e.target.value })
                            }
                        />
                        <input
                            id="discount"
                            placeholder="Desconto atual"
                            type="number"
                            onChange={e =>
                                e.target.value >= 0 && e.target.value <= 1
                                    ? this.setState({
                                          discount: e.target.value,
                                      })
                                    : null
                            }
                        />
                        <input
                            id="max-discount"
                            type="number"
                            placeholder="Desconto máximo"
                            onChange={e =>
                                e.target.value >= 0 && e.target.value <= 1
                                    ? this.setState({
                                          maxDiscount: e.target.value,
                                      })
                                    : null
                            }
                        />
                    </div>
                    <textarea
                        id="description"
                        placeholder="Descrição"
                        type="text"
                        onChange={e =>
                            this.setState({ description: e.target.value })
                        }
                    />
                    {preview === '' ? (
                        <>
                            <label id="media" htmlFor="selecao-arquivo">
                                <FaRegImage id="media-icon" />
                            </label>
                            <input
                                id="selecao-arquivo"
                                type="file"
                                name="pic"
                                onChange={this.handleFile}
                            />
                        </>
                    ) : (
                        <div id="preview-div">
                            <img id="preview" src={preview} alt="" />
                        </div>
                    )}
                </form>
                <FaPaperPlane
                    id="send-icon"
                    onClick={e => this.handlePost(e)}
                />
            </Container>
        );
    }
}
/*
{
	"Name": "Camisa",
	"Description": "Eh uma camisa, ora bolas",
	"Price": 20.0,
	"Stock": 10,
	"CurrentDiscount": 0,
	"MaxDiscount": 0.15,
}
*/
