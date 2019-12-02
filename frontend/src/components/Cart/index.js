import React, { Component } from 'react';
import api from '../../services/api';
import { Container, Item } from './styles';
import { FaMoneyBillWave } from 'react-icons/fa';

export default class Cart extends Component {
    buy = () => {
        const { token, subCart } = this.props;
        api.put(`/cart/finish?AccessToken=${token}&CartId=${subCart.Cart.Id}`)
            .then()
            .catch(() => {
                alert(
                    'Não foi possível completar o pedido, tente novamente mais tarde :-('
                );
            });
    };
    render() {
        const { subCart, redirect } = this.props;
        return (
            <Container>
                <i>de {subCart.Cart.Pro.Nickname}</i>
                {subCart.Products.map((product, index) => (
                    <Item key={index} image={product.Product.Media.URL}>
                        <div id="image" />
                        <div id="col">
                            <div id="row">
                                <p id="amount">{product.Amount}</p>
                                <strong
                                    id="name"
                                    onClick={() =>
                                        redirect('product', product.Product.Id)
                                    }
                                >
                                    {product.Product.Name}
                                </strong>
                            </div>

                            <p id="price">
                                R${' '}
                                {(
                                    product.Product.Price *
                                    product.Amount *
                                    (1 - product.Product.CurrentDiscount)
                                ).toFixed(2)}
                            </p>
                        </div>
                    </Item>
                ))}
                <div id="footer">
                    <p id="total">Total: R$ {subCart.Cart.Total.toFixed(2)}</p>
                    <FaMoneyBillWave id="buy-icon" onClick={this.buy} />
                </div>
            </Container>
        );
    }
}
