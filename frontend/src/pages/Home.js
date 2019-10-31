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
import Topic from '../components/Topic';
import Post from '../components/Post';
import Comment from '../components/Comment';
import Head from '../components/Head';
import Posting from '../components/Posting';
import UserImage from '../components/UserImage';

export default class Home extends Component {
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
        head: '@',
    };

    componentDidMount = () => {
        this.refresh();
    };

    head = () => {
        return (
            <Head
                type={this.state.head}
                me={this.state.user}
                head={this.props.match.params.head}
                refresh={this.refresh}
            />
        );
    };

    refresh = async () => {
        // window.location.reload();
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
            },
            posts,
        });
        const { data: topics2 } = await api.get(
            `/subject?AccessToken=${this.props.match.params.token}`
        );
        topics2.followed = false;
        this.setState({ topics: topics2, posting: false });
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
                        <div id="topics">
                            {this.state.user.topics.map((topic, index) => (
                                <Topic
                                    key={index}
                                    token={this.props.match.params.token}
                                    hashtag={topic.Subject.Hashtag}
                                    topicId={topic.Subject.Id}
                                    followId={topic.UserInterestId}
                                    followed={true}
                                    wallpaper={topic.Subject.Media.URL}
                                />
                            ))}
                            {this.state.topics.map((topic, index) => (
                                <Topic
                                    key={index}
                                    token={this.props.match.params.token}
                                    hashtag={topic.Hashtag}
                                    topicId={topic.Id}
                                    followed={false}
                                    wallpaper={topic.Media.URL}
                                />
                            ))}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
