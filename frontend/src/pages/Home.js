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
import UserHead from '../components/Heads/UserHead';
import TopicHead from '../components/Heads/TopicHead';
import CommentHead from '../components/Heads/CommentHead';
import ProductHead from '../components/Heads/ProductHead';
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
        head: '',
        loaded_head: false,
        feed: {
            comments: [],
            loaded: false,
        },
    };

    componentDidMount = async () => {
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
                ...user,
            },
            posts,
            topics,
            posting: false,
        });
        this.handleHead('', '');
    };

    getHead = async (type, id) => {
        console.log('in getHead');
        const { token } = this.props.match.params;
        const { user: me /*, type, id*/ } = this.state;
        let head = <div />;
        switch (type) {
            case 'u':
                head = <UserHead me={me} userID={id} refresh={this.refresh} />;
                break;
            case 'h':
                head = (
                    <TopicHead
                        me={me}
                        token={token}
                        topicID={id}
                        refresh={this.refresh}
                    />
                );
                break;
            case 'c':
                const { data: comment } = await api.get(`/posts?PostId=${id}`);
                head = (
                    <CommentHead
                        me={me}
                        token={token}
                        comment={comment}
                        refresh={this.refresh}
                    />
                );
                break;
            case '$':
                head = (
                    <ProductHead
                        me={me}
                        token={token}
                        productID={id}
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
        console.log('out getHead');
    };

    getComments = async (type, id) => {
        console.log('in getComments');
        const { token } = this.props.match.params;
        //const { id, type } = this.state;
        let req = '';
        switch (type) {
            case 'h':
                req = `/subject/posts?SubjectId=${id}`;
                break;
            case 'c':
                req = `/post/comments?PostId=${id}`;
                break;
            case 'u':
                req = `/user/posts?UserId=${id}`;
                break;
            default:
                req = `/posts?AccessToken=${token}`;
        }
        const { data: comments } = await api.get(req);
        this.setState({ feed: { comments, loaded: true } });
        console.log('out getComments');
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
            `/media?AccessToken=${token}&IsUserMedia=true`,
            formData,
            config
        );
        this.setState({ user: { image: data[0].URL } });
    };

    handleHead = (type, id) => {
        //console.log('antes ' + type + ' ' + id);
        console.log('in handleHead');
        //this.setState({ type, id });
        //console.log(this.state);
        this.getHead(type, id);
        this.getComments(type, id);
        console.log('out handleHead');
        //console.log('depois ' + this.state.type + ' ' + this.state.id);
    };

    render() {
        const { user, posting, feed, head } = this.state;
        const { token } = this.props.match.params;
        console.log('in render');
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
                                image={user.image.URL}
                            />
                        </label>
                        <input
                            id="new-image"
                            type="file"
                            name="pic"
                            onChange={this.handleImage}
                        ></input>
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
                        <div id="comments">
                            {head}
                            {feed.comments.map((comment, index) => (
                                <Comment
                                    key={index}
                                    me={user}
                                    comment={comment}
                                    token={token}
                                    handleHead={this.handleHead}
                                />
                            ))}
                        </div>
                        <Follows handleHead={this.handleHead} token={token} />
                    </div>
                </div>
            </div>
        );
    }
}
