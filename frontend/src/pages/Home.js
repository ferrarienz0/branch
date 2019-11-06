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
import Posting from '../components/Posting';
import UserImage from '../components/UserImage';
import Feed from '../components/Feed';
import Follows from '../components/Follows';

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
        type: '',
        id: '',
        redirect: false,
        loaded: false,
    };

    componentDidMount = async () => {
        //console.log(localStorage.getItem('token') === null);
        const { token } = this.props.match.params;
        const { data: user } = await api.get(`/user?AccessToken=${token}`);
        const { data: posts } = await api.get(`/posts?AccessToken=${token}`);
        const { data: myTopics } = await api.get(
            `/userInterests?AccessToken=${token}`
        );
        const { data: topics } = await api.get(`/subject?AccessToken=${token}`);
        myTopics.followed = true;
        topics.followed = false;
        this.setState({
            user: {
                ID: user.Id,
                name: decodeURIComponent(user.Firstname),
                lastname: decodeURIComponent(user.Lastname),
                username: decodeURIComponent(user.Nickname),
                email: user.Email,
                image: user.Media,
                topics: myTopics,
            },
            posts,
            topics,
            posting: false,
            loaded: true,
        });
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
        //this.setState({ state: this.state });

        this.forceUpdate();
    };

    render() {
        const { user, posting, type, id, loaded } = this.state;
        const { token } = this.props.match.params;
        console.log('renderizei');
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
                        <UserImage
                            id="user-image"
                            size="100px"
                            image={user.image}
                        />
                        <strong id="user-name">{user.username}</strong>
                        <p id="name">
                            {user.name} {user.lastname}
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
                        {loaded ? (
                            <Feed
                                token={token}
                                handleHead={this.handleHead}
                                me={user}
                                type={type}
                                id={id}
                            />
                        ) : null}
                        <Follows handleHead={this.handleHead} token={token} />
                    </div>
                </div>
            </div>
        );
    }
}
