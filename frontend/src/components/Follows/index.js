import React, { Component } from 'react';
import { Container, User } from './styles';
import { FaAngleRight, FaAngleDown } from 'react-icons/fa';
import Topic from '../Topic';
import Product from '../Product';
import Discount from '../Discount';
import Cart from '../Cart';
import UserImage from '../UserImage';

export default class Follows extends Component {
    state = {
        show: {
            whoIFollow: false,
            myCart: false,
            recommendedProducts: false,
            recommendedDiscount: false,
            myTopics: false,
            recommendedTopics: false,
        },
    };

    whoIFollow = () => {
        const { show } = this.state;
        const { me, redirect } = this.props;
        return (
            <>
                <div
                    id="users"
                    onClick={() =>
                        show.whoIFollow
                            ? this.setState({
                                  show: { ...show, whoIFollow: false },
                              })
                            : this.setState({
                                  show: { ...show, whoIFollow: true },
                              })
                    }
                >
                    {show.whoIFollow ? <FaAngleDown /> : <FaAngleRight />}
                    Quem eu sigo
                </div>

                {show.whoIFollow
                    ? me.users.map((user, index) => (
                          <User
                              onClick={() => redirect('user', user.Id)}
                              key={index}
                          >
                              <UserImage size="30px" image={user.Media.URL} />
                              <div id="name">@{user.Nickname}</div>
                          </User>
                      ))
                    : null}
            </>
        );
    };

    myCart = () => {
        const { show } = this.state;
        const { me, token, redirect } = this.props;
        return (
            <>
                <div
                    id="cart"
                    onClick={() =>
                        show.myCart
                            ? this.setState({
                                  show: { ...show, myCart: false },
                              })
                            : this.setState({ show: { ...show, myCart: true } })
                    }
                >
                    {show.myCart ? <FaAngleDown /> : <FaAngleRight />}
                    Meu carrinho
                </div>
                {show.myCart
                    ? me.cart.map((subCart, index) => (
                          <Cart
                              key={index}
                              token={token}
                              subCart={subCart}
                              redirect={redirect}
                          />
                      ))
                    : null}
            </>
        );
    };

    recommendedProducts = () => {
        const { show } = this.state;
        const { cart, token, redirect } = this.props;
        return (
            <>
                <div
                    id="cart"
                    onClick={() =>
                        show.recommendedProducts
                            ? this.setState({
                                  show: { ...show, recommendedProducts: false },
                              })
                            : this.setState({
                                  show: { ...show, recommendedProducts: true },
                              })
                    }
                >
                    {show.recommendedProducts ? (
                        <FaAngleDown />
                    ) : (
                        <FaAngleRight />
                    )}
                    Produtos recomendados
                </div>
                {show.recommendedProducts
                    ? cart.map((product, index) => (
                          <Product
                              key={index}
                              token={token}
                              product={product}
                              redirect={redirect}
                          />
                      ))
                    : null}
            </>
        );
    };

    recommendedDiscounts = () => {
        const { show } = this.state;
        const { me, discount, token, redirect } = this.props;
        return me.pro ? (
            <>
                <div
                    id="cart"
                    onClick={() =>
                        show.recommendedDiscount
                            ? this.setState({
                                  show: { ...show, recommendedDiscount: false },
                              })
                            : this.setState({
                                  show: { ...show, recommendedDiscount: true },
                              })
                    }
                >
                    {show.recommendedDiscount ? (
                        <FaAngleDown />
                    ) : (
                        <FaAngleRight />
                    )}
                    Descontos recomendados
                </div>
                {show.recommendedDiscount
                    ? discount.map((discount, index) => (
                          <Discount
                              key={index}
                              token={token}
                              productID={discount.ProductId}
                              discount={discount.RecommendedDiscount}
                              redirect={redirect}
                          />
                      ))
                    : null}
            </>
        ) : null;
    };

    myTopics = () => {
        const { show } = this.state;
        const { me, token, redirect } = this.props;
        return (
            <>
                <div
                    id="topics"
                    onClick={() =>
                        show.myTopics
                            ? this.setState({
                                  show: { ...show, myTopics: false },
                              })
                            : this.setState({
                                  show: { ...show, myTopics: true },
                              })
                    }
                >
                    {show.myTopics ? <FaAngleDown /> : <FaAngleRight />}
                    Meus tópicos
                </div>
                {show.myTopics
                    ? me.topics.map((topic, index) => (
                          <Topic
                              key={index}
                              head={false}
                              token={token}
                              topic={{
                                  ID: topic.Id,
                                  hashtag: topic.Hashtag,
                                  banner: topic.Media.URL,
                                  follow: true,
                              }}
                              redirect={() => redirect('topic', topic.Id)}
                          />
                      ))
                    : null}
            </>
        );
    };

    recommendedTopics = () => {
        const { show } = this.state;
        const { topics, token, redirect } = this.props;
        return (
            <>
                <div
                    id="topics"
                    onClick={() =>
                        show.recommendedTopics
                            ? this.setState({
                                  show: { ...show, recommendedTopics: false },
                              })
                            : this.setState({
                                  show: { ...show, recommendedTopics: true },
                              })
                    }
                >
                    {show.recommendedTopics ? (
                        <FaAngleDown />
                    ) : (
                        <FaAngleRight />
                    )}
                    Tópicos recomendados
                </div>
                {show.recommendedTopics
                    ? topics.map((topic, index) => (
                          <Topic
                              key={index}
                              head={false}
                              token={token}
                              topic={{
                                  ID: topic.Id,
                                  hashtag: topic.Hashtag,
                                  banner:
                                      topic.Media === null
                                          ? null
                                          : topic.Media.URL,
                                  follow: false,
                              }}
                              redirect={() => redirect('topic', topic.Id)}
                          />
                      ))
                    : null}
            </>
        );
    };

    render() {
        return (
            <Container>
                {this.whoIFollow()}
                {this.myCart()}
                {this.recommendedProducts()}
                {this.recommendedDiscounts()}
                {this.myTopics()}
                {this.recommendedTopics()}
            </Container>
        );
    }
}
