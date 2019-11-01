import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import {
    FaPaperPlane,
    FaPaperclip,
    FaShoppingCart,
    FaPowerOff,
    FaComment,
} from 'react-icons/fa';
import './Explore.css';
import icone from '../assets/icone.svg';
import api from '../services/api';
import Topic from '../components/Topic';
import Comment from '../components/Comment';
import UserHead from '../components/Heads/UserHead';
import TopicHead from '../components/Heads/TopicHead';
import CommentHead from '../components/Heads/CommentHead';
import ProductHead from '../components/Heads/ProductHead';
import Posting from '../components/Posting';
import UserImage from '../components/UserImage';

export default class Explore extends Component {
    state = {
        posting: false,
        user: {
            ID: -2,
            name: '',
            lastname: '',
            username: '',
            email: '',
            image: '',
            topics: [],
        },
        posts: [],
        topics: [],
        type: '',
        id: '',
        goAhead: false,
    };

    componentDidMount = async () => {
        console.log('oi');
        const { data: userData } = await api.get(
            `/user?AccessToken=${this.props.match.params.token}`
        );

        const { data: posts } = await api.get(
            `/posts?AccessToken=${this.props.match.params.token}`
        );

        const { data: topics } = await api.get(
            `/userInterests?AccessToken=${this.props.match.params.token}`
        );
        topics.followed = true;
        this.setState({
            user: {
                ID: userData.Id,
                name: decodeURIComponent(userData.Firstname),
                lastname: decodeURIComponent(userData.Lastname),
                username: decodeURIComponent(userData.Nickname),
                email: userData.Email,
                topics,
                type: this.props.match.params.type,
                id: this.props.match.params.id,
            },
            posts,
        });
        const { data: topics2 } = await api.get(
            `/subject?AccessToken=${this.props.match.params.token}`
        );
        console.log(this.state.type);
        topics2.followed = false;
        this.setState({ topics: topics2, posting: false });
    };

    handleGoAhead = (type, id) => {
        this.setState({ type, id, goAhead: true });
    };

    head = () => {
        switch (this.state.type) {
            case '@':
                return (
                    <UserHead
                        me={this.state.user}
                        userID={this.state.id}
                        refresh={this.refresh}
                    />
                );
            case '#':
                return (
                    <TopicHead
                        me={this.state.user}
                        topicID={this.props.match.params.id}
                        refresh={this.refresh}
                    />
                );
            case 'c':
                return (
                    <CommentHead
                        me={this.state.user}
                        commentID={this.state.id}
                        refresh={this.refresh}
                    />
                );
            case '$':
                return (
                    <ProductHead
                        me={this.state.user}
                        productID={this.state.id}
                        refresh={this.refresh}
                    />
                );
            default:
                return <UserHead me={this.state.user} refresh={this.refresh} />;
        }
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

    handleUserPost = () => {};
    render() {
        if (this.state.goAhead) {
            return (
                <Redirect
                    to={`/home/${this.props.match.params.token}/${this.state.type}/${this.state.id}`}
                />
            );
        }
        return (
            <div id="home-container">
                <div id="home-head">
                    <div id="logo">
                        <img src={icone} alt="Branch"></img>
                        <Link
                            id="go-home"
                            to={`/home/${this.props.match.params.token}`}
                        />
                    </div>
                    <div id="space" />
                    <Link to="/">
                        <FaPowerOff id="logoff" />
                    </Link>
                </div>
                <div id="home-body">
                    <div id="perfil">
                        <UserImage
                            id="user-image"
                            size="100px"
                            image={this.state.user.image}
                        />
                        <strong id="user-name">
                            {this.state.user.username}
                        </strong>
                        <p id="name">
                            {this.state.user.name} {this.state.user.lastname}
                        </p>
                        <FaComment
                            id="comment-icon"
                            onClick={e =>
                                this.state.posting
                                    ? this.setState({ posting: false })
                                    : this.setState({ posting: true })
                            }
                        />
                        <FaShoppingCart id="cart-icon" />
                    </div>
                    <div id="feed">
                        <div id="posts">
                            {this.head()}
                            {this.state.posting ? (
                                <Posting
                                    user={this.state.user.username}
                                    token={this.props.match.params.token}
                                    onClose={() =>
                                        this.setState({ posting: false })
                                    }
                                />
                            ) : null}
                            {this.state.posts.map((comment, index) => (
                                <Comment
                                    key={index}
                                    me={this.state.user}
                                    comment={comment}
                                    token={this.props.match.params.token}
                                    refresh={this.refresh}
                                />
                            ))}
                            {/*this.state.posts.map((post, index) => (
                                <Post
                                    key={index}
                                    post={post}
                                    user={this.state.user}
                                    token={this.props.match.params.token}
                                />
                            ))*/}
                        </div>
                        <div id="follows">
                            <div id="topics">Meus tópicos</div>
                            {this.state.user.topics.map((topic, index) => (
                                <div
                                    key={index}
                                    onClick={() => {
                                        this.handleGoAhead(
                                            '#',
                                            topic.Subject.Id
                                        );
                                    }}
                                >
                                    <Topic
                                        token={this.props.match.params.token}
                                        hashtag={topic.Subject.Hashtag}
                                        topicId={topic.Subject.Id}
                                        followId={topic.UserInterestId}
                                        followed={true}
                                        wallpaper={topic.Subject.Media.URL}
                                    />
                                </div>
                            ))}
                            <div id="topics">Recomendado</div>
                            {this.state.topics.map((topic, index) => (
                                <div
                                    key={index}
                                    onClick={() => {
                                        this.handleGoAhead('#', topic.Id);
                                    }}
                                >
                                    <Topic
                                        key={index}
                                        token={this.props.match.params.token}
                                        hashtag={topic.Hashtag}
                                        topicId={topic.Id}
                                        followed={false}
                                        wallpaper={topic.Media.URL}
                                    />
                                </div>
                            ))}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
