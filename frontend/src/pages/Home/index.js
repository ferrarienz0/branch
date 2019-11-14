import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { FaShoppingCart, FaPowerOff, FaComment } from 'react-icons/fa';
import { Container, Header, Body, Perfil, Explore } from './styles.js';
import icone from '../../assets/icone.svg';
import api from '../../services/api';
import Posting from '../../components/Posting';
import UserImage from '../../components/UserImage';
import Feed from '../../components/Feed';
import UserHead from '../../components/Heads/UserHead';
import TopicHead from '../../components/Heads/TopicHead';
import Comment from '../../components/Comment';
import ProductHead from '../../components/Heads/ProductHead';
import Follows from '../../components/Follows';

export default class Home extends Component {
    state = {
        me: {
            ID: 0,
            name: '',
            lastName: '',
            userName: '',
            email: '',
            image: '',
            users: [],
            topics: [],
        },
        topics: [],
        posting: false,
        type: '',
        id: '',
        redirect: false,
        loaded: false,
        head: <div />,
        loaded_head: false,
        feed: {
            comments: [],
            loaded: false,
        },
    };

    componentDidMount = async () => {
        const { token } = this.props.match.params;
        const { data: user } = await api.get(`/user?AccessToken=${token}`);
        const { data: me_users } = await api.get(
            `/follows?AccessToken=${token}`
        );
        const { data: me_topics } = await api.get(
            `/subjects/followed?AccessToken=${token}`
        );
        const { data: topics } = await api.get(
            `/subjects/unfollowed?AccessToken=${token}`
        );
        this.setState({
            me: {
                ID: user.Id,
                name: decodeURIComponent(user.Firstname),
                lastName: decodeURIComponent(user.Lastname),
                userName: decodeURIComponent(user.Nickname),
                email: user.Email,
                image: user.Media,
                users: me_users,
                topics: me_topics,
            },
            topics,
        });
        this.handleHead('', '');
    };

    setHeadToUser = async id => {
        const { token } = this.props.match.params;
        const { me } = this.state;
        const { data: user } = await api.get(`/user?UserId=${id}`);
        const { data: comments } = await api.get(`/posts/user?UserId=${id}`);
        this.setState({
            head: (
                <UserHead
                    me={me}
                    user={user}
                    token={token}
                    refresh={this.refresh}
                />
            ),
            loaded_head: true,
            feed: { comments, loaded: true },
        });
    };

    setHeadToTopic = async id => {
        const { token } = this.props.match.params;
        const { me } = this.state;
        const { data: topic } = await api.get(`/subject?SubjectId=${id}`);
        const { data: comments } = await api.get(
            `/posts/subject?SubjectId=${id}`
        );
        this.setState({
            head: (
                <TopicHead
                    me={me}
                    topic={{
                        ID: topic.Id,
                        hashtag: topic.Hashtag,
                        banner: topic.Media.URL,
                    }}
                    token={token}
                    refresh={this.refresh}
                />
            ),
            loaded_head: true,
            feed: { comments, loaded: true },
        });
    };

    setHeadToComment = async id => {
        const { token } = this.props.match.params;
        const { me } = this.state;
        const { data: comment } = await api.get(`/post?PostId=${id}`);
        const { data: comments } = await api.get(`/post/comments?PostId=${id}`);
        this.setState({
            head: (
                <Comment
                    head={true}
                    me={me}
                    comment={comment}
                    token={token}
                    refresh={this.refresh}
                />
            ),
            loaded_head: true,
            feed: { comments, loaded: true },
        });
    };

    setHeadToProduct = async id => {
        const { token } = this.props.match.params;
        const { me } = this.state;
        const { data: product } = await api.get(`/product?ProductId=${id}`);
        const { data: comments } = await api.get(
            `/posts/product?ProductId=${id}`
        );
        this.setState({
            head: (
                <ProductHead
                    me={me}
                    product={product}
                    token={token}
                    refresh={this.refresh}
                />
            ),
            loaded_head: true,
            feed: { comments, loaded: true },
        });
    };

    setHeadToDefault = async () => {
        const { token } = this.props.match.params;
        const { data: comments } = await api.get(`/posts?AccessToken=${token}`);
        this.setState({
            head: <div />,
            loaded_head: true,
            feed: { comments, loaded: true },
        });
    };

    handleImage = async e => {
        const { token } = this.props.match.params;
        let formData = new FormData();
        formData.append('image', e.target.files[0]);
        let config = {
            headers: {
                Accept: '',
                'Content-Type': 'multipart/form-data',
            },
        };
        const { data } = await api.post(
            `/media/create?AccessToken=${token}&IsUserMedia=true`,
            formData,
            config
        );
        this.setState({ me: { image: data[0].URL, ...this.state.me } });
    };

    handleHead = (type, id) => {
        switch (type) {
            case 'user':
                this.setHeadToUser(id);
                break;
            case 'topic':
                this.setHeadToTopic(id);
                break;
            case 'comment':
                this.setHeadToComment(id);
                break;
            case 'product':
                this.setHeadToProduct(id);
                break;
            default:
                this.setHeadToDefault();
        }
    };

    render() {
        const { me, posting, feed, head, topics } = this.state;
        const { token } = this.props.match.params;
        return (
            <Container>
                <Header>
                    <div id="logo" onClick={() => this.handleHead('', '')}>
                        <img src={icone} alt="Branch"></img>
                    </div>
                    <div id="space" />
                    <Link to="/">
                        <FaPowerOff id="logoff" />
                    </Link>
                </Header>
                {posting ? (
                    <Posting
                        token={token}
                        onClose={() => this.setState({ posting: false })}
                    />
                ) : null}
                <Body>
                    <Perfil>
                        <label htmlFor="new-image">
                            <UserImage
                                id="user-image"
                                size="100px"
                                image={me.image.URL}
                            />
                        </label>
                        <input
                            id="new-image"
                            type="file"
                            name="pic"
                            onChange={this.handleImage}
                        ></input>
                        <strong id="user-name">{me.userName}</strong>
                        <p id="name">
                            {me.name} {me.lastName}
                        </p>
                        <FaComment
                            id="comment-icon"
                            onClick={() =>
                                posting
                                    ? this.setState({ posting: false })
                                    : this.setState({ posting: true })
                            }
                        />
                        <FaShoppingCart id="cart-icon" />
                    </Perfil>
                    <Explore>
                        <Feed
                            me={me}
                            token={token}
                            head={head}
                            feed={feed}
                            handleHead={this.handleHead}
                        />
                        <Follows
                            me={me}
                            topics={topics}
                            token={token}
                            handleHead={this.handleHead}
                        />
                    </Explore>
                </Body>
            </Container>
        );
    }
}
