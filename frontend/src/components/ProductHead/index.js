import React, { Component } from 'react';
import { FaCartPlus } from 'react-icons/fa';
import { Container, Header, Body, Footer } from './styles';

export default class ProductHead extends Component {
    render() {
        const { me, product, handleHead } = this.props;
        return (
            <Container>
                <Header>
                    <strong id="name">${product.name}</strong>
                    <i>
                        por{' '}
                        <strong
                            id="pro-name"
                            onClick={() => handleHead('user', product.pro.ID)}
                        >
                            @{product.pro.userName}
                        </strong>
                    </i>
                </Header>
                <Body>
                    <img src={product.image} alt="" />
                    <div id="right">
                        <strong id="price">
                            R$ {product.price.toFixed(2)}
                        </strong>
                        <p id="description">{product.description}</p>
                    </div>
                </Body>
                {me.ID === product.pro.ID ? null : (
                    <Footer>
                        <FaCartPlus id="add-cart" />
                    </Footer>
                )}
            </Container>
        );
    }
}
