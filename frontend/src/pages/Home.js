import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import {
    FaPaperPlane,
    FaPaperclip,
    FaShoppingCart,
    FaPowerOff,
    FaComment,
} from 'react-icons/fa';
import './Home.css';
import icone from '../assets/icone.svg';
import api from '../services/api';
import Posting from '../components/Posting';
import UserImage from '../components/UserImage';
//import Feed from '../components/Feed';
import Comment from '../components/Comment';
import Topic from '../components/Topic';
import UserHead from '../components/Heads/UserHead';
import TopicHead from '../components/Heads/TopicHead';
import CommentHead from '../components/Heads/CommentHead';
import ProductHead from '../components/Heads/ProductHead';
//import Follows from '../components/Follows';

export default class Home extends Component {
    state = {
        me: {
            ID: -2,
            name: '',
            lastname: '',
            username: '',
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
        head: '',
        loaded_head: false,
        feed: {
            comments: [],
            loaded: false,
        },
    };

    componentDidMount = async () => {
        const { token } = this.props.match.params;
        //get logado
        const { data: user } = await api.get(`/user?AccessToken=${token}`);
        //get usuários que logado segue
        const { data: me_users } = await api.get(
            `/follows?AccessToken=${token}`
        );
        //get tópicos que logado segue
        const { data: me_topics } = await api.get(
            `/subjects/followed?AccessToken=${token}`
        );
        //get tópicos que logado não segue
        const { data: topics } = await api.get(
            `/subjects/unfollowed?AccessToken=${token}`
        );

        this.setState({
            me: {
                ID: user.Id,
                name: decodeURIComponent(user.Firstname),
                lastname: decodeURIComponent(user.Lastname),
                username: decodeURIComponent(user.Nickname),
                email: user.Email,
                image: user.Media,
                users: me_users,
                topics: me_topics,
            },
            topics,
            posting: false,
        });
        this.handleHead('', '');
    };

    getHead = async (type, id) => {
        const { token } = this.props.match.params;
        const { me } = this.state;
        let head = <div />;
        switch (type) {
            case 'user':
                const { data: user } = await api.get(`/user?UserId=${id}`);
                head = <UserHead me={me} user={user} refresh={this.refresh} />;
                break;
            case 'topic':
                const { data: topic } = await api.get(
                    `/subject?SubjectId=${id}`
                );
                head = (
                    <TopicHead
                        me={me}
                        token={token}
                        topic={{
                            ID: topic.Id,
                            hashtag: topic.Hashtag,
                            banner: topic.Media.URL,
                        }}
                        refresh={this.refresh}
                    />
                );
                break;
            case 'comment':
                const { data: comment } = await api.get(`/post?PostId=${id}`);
                head = (
                    <CommentHead
                        me={me}
                        token={token}
                        comment={comment}
                        refresh={this.refresh}
                    />
                );
                break;
            case 'product':
                const { data: product } = await api.get(
                    `/product?ProductId=${id}`
                );
                head = (
                    <ProductHead
                        me={me}
                        token={token}
                        product={product}
                        refresh={this.refresh}
                    />
                );
                break;
            default:
                head = <div />;
        }
        this.setState({
            head,
            loaded_head: true,
        });
    };

    getComments = async (type, id) => {
        const { token } = this.props.match.params;
        let req = '';
        switch (type) {
            case 'user':
                req = `/posts/user?UserId=${id}`;
                break;
            case 'topic':
                req = `/posts/subject?SubjectId=${id}`;
                break;
            case 'comment':
                req = `/post/comments?PostId=${id}`;
                break;
            case 'product':
                req = `/posts/product?ProductId=${id}`;
                break;
            default:
                req = `/posts?AccessToken=${token}`;
        }
        const { data: comments } = await api.get(req);
        this.setState({ feed: { comments, loaded: true } });
    };

    refresh = () => {
        window.location.reload();
    };

    showPosting = () => {
        return (
            <div id="posting-container">
                <textarea type="text" />
                <div className="optionsPost">
                    <button className="send">
                        <FaPaperPlane className="sendIcon" />
                    </button>
                    <FaPaperclip className="clipIcon" />
                </div>
            </div>
        );
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
        this.getHead(type, id);
        this.getComments(type, id);
    };

    render() {
        const { me, posting, feed, head, topics } = this.state;
        const { token } = this.props.match.params;
        return (
            <div id="home-container">
                <div id="home-head">
                    <div id="logo">
                        <img src={icone} alt="Branch"></img>
                        <Link id="go-home" to={`/home/${token}`} />
                    </div>
                    <div id="space" />
                    <Link to="/">
                        <FaPowerOff id="logoff" />
                    </Link>
                </div>
                {this.state.posting ? (
                    <Posting
                        token={token}
                        onClose={() => this.setState({ posting: false })}
                    />
                ) : null}
                <div id="home-body">
                    <div id="perfil">
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
                        <strong id="user-name">{me.username}</strong>
                        <p id="name">
                            {me.name} {me.lastname}
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
                    </div>
                    <div id="feed">
                        <div id="comments">
                            {head}
                            {feed.comments.map((comment, index) => (
                                <Comment
                                    key={index}
                                    me={me}
                                    comment={comment}
                                    token={token}
                                    onHead={() =>
                                        this.handleHead('comment', comment.Id)
                                    }
                                />
                            ))}
                        </div>
                        <div id="follows">
                            <div id="users">Quem eu sigo</div>
                            {me.users.map((user, index) => (
                                <div
                                    id="user"
                                    onClick={() =>
                                        this.handleHead('user', user.Id)
                                    }
                                    key={index}
                                >
                                    <UserImage size="30px" image={user.Media} />
                                    <div id="name">@{user.Nickname}</div>
                                </div>
                            ))}
                            <div id="topics">Meus tópicos</div>
                            {me.topics.map((topic, index) => (
                                <Topic
                                    key={index}
                                    token={token}
                                    topic={{
                                        id: topic.Id,
                                        hashtag: topic.Hashtag,
                                        banner: topic.Media.URL,
                                        follow: true,
                                    }}
                                    refresh={this.refresh}
                                    onHead={() =>
                                        this.handleHead('topic', topic.Id)
                                    }
                                />
                            ))}
                            <div id="topics">Recomendado</div>
                            {topics.map((topic, index) => (
                                <Topic
                                    key={index}
                                    token={token}
                                    topic={{
                                        id: topic.Id,
                                        hashtag: topic.Hashtag,
                                        banner: topic.Media.URL,
                                        follow: false,
                                    }}
                                    refresh={this.refresh}
                                    onHead={() =>
                                        this.handleHead('topic', topic.Id)
                                    }
                                />
                            ))}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
