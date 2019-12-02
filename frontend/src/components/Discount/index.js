import React, { Component } from 'react';
import { Container } from './styles';
import api from '../../services/api';

export default class Discount extends Component {
    state = {
        product: {},
    };

    componentDidMount = async () => {
        const { productID } = this.props;
        const { data: product } = await api.get(
            `/product?ProductId=${productID}`
        );
        this.setState({ product });
    };

    apply = async () => {
        const { token, productID, discount } = this.props;
        await api.put(
            `/product/discount?AccessToken=${token}&ProductId=${productID}&Discount=${discount}`
        );
    };

    render() {
        const { product } = this.state;
        const { discount, redirect } = this.props;
        return (
            <Container
                image={product.Media === undefined ? null : product.Media.URL}
            >
                <div id="image" />
                <div id="col">
                    <div id="row">
                        <strong
                            id="name"
                            onClick={() => redirect('product', product.Id)}
                        >
                            ${product.Name}
                        </strong>
                    </div>
                    <p id="price">
                        de R${' '}
                        {(
                            product.Price *
                            (1 - product.CurrentDiscount)
                        ).toFixed(2)}{' '}
                        para {(product.Price * (1 - discount)).toFixed(2)}
                    </p>
                </div>
                <i onClick={this.apply}>Aplicar</i>
            </Container>
        );
    }
}
