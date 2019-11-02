import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
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
import Comment from '../components/Comment';
import UserHead from '../components/Heads/UserHead';
import TopicHead from '../components/Heads/TopicHead';
import CommentHead from '../components/Heads/CommentHead';
import ProductHead from '../components/Heads/ProductHead';
import Posting from '../components/Posting';
import UserImage from '../components/UserImage';

export default class Home extends Component {
    state = {
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
        posting: false,
        reload: false,
        type: '',
        id: '',
        //goAhead: false,
    };

    componentDidMount = async () => {
        const { params } = this.props.match;
        const { data: user } = await api.get(
            `/user?AccessToken=${params.token}`
        );
        const { data: posts } = await api.get(
            `/posts?AccessToken=${params.token}`
        );
        const { data: myTopics } = await api.get(
            `/userInterests?AccessToken=${params.token}`
        );
        const { data: topics } = await api.get(
            `/subject?AccessToken=${params.token}`
        );
        myTopics.followed = true;
        topics.followed = false;
        this.setState({
            user: {
                ID: user.Id,
                name: decodeURIComponent(user.Firstname),
                lastname: decodeURIComponent(user.Lastname),
                username: decodeURIComponent(user.Nickname),
                email: user.Email,
                topics: myTopics,
            },
            posts,
            topics,
            posting: false,
        });
        console.log('entrei');
        //this.setState({
        //    type: this.props.match.params.type,
        //    id: this.props.match.params.id,
        //});
    };

    head = () => {
        const { params } = this.props.match;
        switch (params.type) {
            case 'u':
                return (
                    <UserHead
                        me={this.state.user}
                        userID={params.id}
                        refresh={this.refresh}
                    />
                );
            case 'h':
                return (
                    <TopicHead
                        me={this.state.user}
                        topicID={params.id}
                        refresh={this.refresh}
                    />
                );
            case 'c':
                return (
                    <CommentHead
                        me={this.state.user}
                        commentID={params.id}
                        refresh={this.refresh}
                    />
                );
            case '$':
                return (
                    <ProductHead
                        me={this.state.user}
                        productID={params.id}
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
        const { params } = this.props.match;
        //if (this.state.reload) {
        //    this.setState({ reload: false });
        //    return (
        //        <Redirect
        //            to={`/home/${params.token}/${this.state.type}/${this.state.id}`}
        //        />
        //    );
        //}
        return (
            <div id="home-container">
                <div id="home-head">
                    <div id="logo">
                        <img src={icone} alt="Branch"></img>
                        <Link
                            id="go-home"
                            to={`/home/${params.token}/u/${this.state.user.ID}`}
                        />
                    </div>
                    <div id="space" />
                    <Link to="/">
                        <FaPowerOff id="logoff" />
                    </Link>
                </div>
                {this.state.posting ? (
                    <Posting
                        user={this.state.user.username}
                        token={params.token}
                        onClose={() => this.setState({ posting: false })}
                    />
                ) : null}
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

                            {this.state.posts.map((comment, index) => (
                                <Comment
                                    key={index}
                                    me={this.state.user}
                                    comment={comment}
                                    token={params.token}
                                    refresh={this.refresh}
                                />
                            ))}
                            {/*this.state.posts.map((post, index) => (
                                <Post
                                    key={index}
                                    post={post}
                                    user={this.state.user}
                                    token={params.token}
                                />
                            ))*/}
                        </div>
                        <div id="follows">
                            <div id="topics">Meus tópicos</div>
                            {this.state.user.topics.map((topic, index) => (
                                <Topic
                                    key={index}
                                    token={params.token}
                                    hashtag={topic.Subject.Hashtag}
                                    topicId={topic.Subject.Id}
                                    followId={topic.UserInterestId}
                                    followed={true}
                                    wallpaper={topic.Subject.Media.URL}
                                    refresh={this.refresh}
                                />
                            ))}
                            <div id="topics">Recomendado</div>
                            {this.state.topics.map((topic, index) => (
                                <Link
                                    to={`/home/${this.props.match.params.token}/h/${topic.Id}`}
                                    key={index}
                                >
                                    <Topic
                                        key={index}
                                        token={params.token}
                                        hashtag={topic.Hashtag}
                                        topicId={topic.Id}
                                        followed={false}
                                        wallpaper={topic.Media.URL}
                                        refresh={this.refresh}
                                    />
                                </Link>
                            ))}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
