import React, { Component } from 'react';
import { Container, User } from './styles';
import { FaAngleRight, FaAngleDown } from 'react-icons/fa';
import Topic from '../../components/Topic';
import Product from '../../components/Product';
import UserImage from '../../components/UserImage';

export default class Follows extends Component {
    state = {
        showUsers: false,
        showCart: false,
        showTopics: false,
        showRecommended: false,
    };
    render() {
        const { showUsers, showCart, showTopics, showRecommended } = this.state;
        const { me, topics, handleHead, token } = this.props;
        return (
            <Container>
                <div
                    id="users"
                    onClick={() =>
                        showUsers
                            ? this.setState({ showUsers: false })
                            : this.setState({ showUsers: true })
                    }
                >
                    {showUsers ? <FaAngleDown /> : <FaAngleRight />}
                    Quem eu sigo
                </div>
                {showUsers
                    ? me.users.map((user, index) => (
                          <User
                              onClick={() => handleHead('user', user.Id)}
                              key={index}
                          >
                              <UserImage size="30px" image={user.Media} />
                              <div id="name">@{user.Nickname}</div>
                          </User>
                      ))
                    : null}
                <div
                    id="cart"
                    onClick={() =>
                        showCart
                            ? this.setState({ showCart: false })
                            : this.setState({ showCart: true })
                    }
                >
                    {showCart ? <FaAngleDown /> : <FaAngleRight />}
                    Meu carrinho
                </div>
                {/* {me.cart.map((product, index) => (
                    <Product
                        key={index}
                        token={token}
                        product={product}
                        onHead={() => handleHead('topic', product.Id)}
                    />
                ))} */}
                <div
                    id="topics"
                    onClick={() =>
                        showTopics
                            ? this.setState({ showTopics: false })
                            : this.setState({ showTopics: true })
                    }
                >
                    {showTopics ? <FaAngleDown /> : <FaAngleRight />}
                    Meus tópicos
                </div>
                {showTopics
                    ? me.topics.map((topic, index) => (
                          <Topic
                              key={index}
                              head={false}
                              token={token}
                              topic={{
                                  id: topic.Id,
                                  hashtag: topic.Hashtag,
                                  banner: topic.Media.URL,
                                  follow: true,
                              }}
                              onHead={() => handleHead('topic', topic.Id)}
                          />
                      ))
                    : null}
                <div
                    id="topics"
                    onClick={() =>
                        showRecommended
                            ? this.setState({ showRecommended: false })
                            : this.setState({ showRecommended: true })
                    }
                >
                    {showRecommended ? <FaAngleDown /> : <FaAngleRight />}
                    Recomendado
                </div>
                {showRecommended
                    ? topics.map((topic, index) => (
                          <Topic
                              key={index}
                              head={false}
                              token={token}
                              topic={{
                                  id: topic.Id,
                                  hashtag: topic.Hashtag,
                                  banner:
                                      topic.Media === null
                                          ? null
                                          : topic.Media.URL,
                                  follow: false,
                              }}
                              onHead={() => handleHead('topic', topic.Id)}
                          />
                      ))
                    : null}
            </Container>
        );
    }
}
