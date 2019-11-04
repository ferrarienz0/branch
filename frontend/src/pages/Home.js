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
import UserHead from '../components/Heads/UserHead';
import TopicHead from '../components/Heads/TopicHead';
import CommentHead from '../components/Heads/CommentHead';
import ProductHead from '../components/Heads/ProductHead';
import Posting from '../components/Posting';
import UserImage from '../components/UserImage';
import Feed from '../components/Feed';

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
    };

    head = () => {};
    ahead = () => {
        const { token, type, id } = this.props.match.params;
        //const { type, id } = this.state;
        switch (type) {
            case 'u':
                return (
                    <UserHead
                        me={this.state.user}
                        userID={id}
                        refresh={this.refresh}
                    />
                );
            case 'h':
                console.log('head: type ' + type + ', id ' + id);
                return (
                    <TopicHead
                        me={this.state.user}
                        token={token}
                        topicID={id}
                        refresh={this.refresh}
                    />
                );
            case 'c':
                return (
                    <CommentHead
                        me={this.state.user}
                        token={token}
                        commentID={id}
                        refresh={this.refresh}
                    />
                );
            case '$':
                return (
                    <ProductHead
                        me={this.state.user}
                        productID={id}
                        refresh={this.refresh}
                    />
                );
            default:
            //return (
            //    <UserHead
            //        me={this.state.user}
            //        userID={this.state.user.ID}
            //        refresh={this.refresh}
            //    />
            //);
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

    handleHead = (type, id) => {
        console.log(type + ' ' + id);
        this.setState({ type, id });
    };

    render() {
        const { token, type, id } = this.props.match.params;
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
                        user={this.state.user.username}
                        token={token}
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
                        <Feed
                            token={token}
                            me={this.state.user}
                            type={type}
                            id={id}
                        />
                        <div id="follows">
                            <div id="topics">Meus tópicos</div>
                            {this.state.user.topics.map((topic, index) => (
                                <Topic
                                    key={index}
                                    token={token}
                                    hashtag={topic.Subject.Hashtag}
                                    topicID={topic.Subject.Id}
                                    followID={topic.UserInterestId}
                                    followed={true}
                                    wallpaper={topic.Subject.Media.URL}
                                    handleHead={this.handleHead}
                                />
                            ))}
                            <div id="topics">Recomendado</div>
                            {this.state.topics.map((topic, index) => (
                                <Topic
                                    key={index}
                                    token={token}
                                    hashtag={topic.Hashtag}
                                    topicID={topic.Id}
                                    followed={false}
                                    wallpaper={topic.Media.URL}
                                    refresh={this.refresh}
                                    handleHead={this.handleHead}
                                />
                            ))}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
