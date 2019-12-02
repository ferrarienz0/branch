import React, { Component } from 'react';
import { FaCartPlus } from 'react-icons/fa';
import api from '../../services/api';
import { Container, Header, Body, Footer } from './styles';

export default class ProductHead extends Component {
    state = {
        units: 0,
    };
    addToCart = async () => {
        const { units } = this.state;
        const { token, product } = this.props;
        await api.post(`/cart/insert?AccessToken=${token}`, {
            ProId: product.pro.ID,
            ProductId: product.ID,
            Amount: units,
        });
    };
    render() {
        const { me, product, redirect } = this.props;
        return (
            <Container>
                <Header>
                    <strong id="name">${product.name}</strong>
                    <i>
                        por{' '}
                        <strong
                            id="pro-name"
                            onClick={() => redirect('user', product.pro.ID)}
                        >
                            @{product.pro.userName}
                        </strong>
                    </i>
                </Header>
                <Body>
                    <img src={product.image} alt="" />
                    <div id="right">
                        {product.discount === 0 ? null : (
                            <p id="description">
                                de R$ {product.price.toFixed(2)} por
                            </p>
                        )}
                        <strong id="price">
                            R$ {(product.price - product.discount).toFixed(2)}
                        </strong>
                        <p id="description">{product.description}</p>
                    </div>
                </Body>
                {me.ID === product.pro.ID ? null : (
                    <Footer>
                        <input
                            type="number"
                            placeholder="0"
                            min="0"
                            max={product.stock}
                            onChange={e =>
                                this.setState({ units: e.target.value })
                            }
                        />
                        <FaCartPlus id="add-cart" onClick={this.addToCart} />
                        <p id="stock">{product.stock} unid. disponíveis</p>
                    </Footer>
                )}
            </Container>
        );
    }
}
