import React, { Component } from 'react';
import {
    FaThumbsUp,
    FaThumbsDown,
    FaRegThumbsUp,
    FaRegThumbsDown,
    FaComment,
    FaComments,
    FaCartPlus,
    FaPlus,
    FaTimes,
} from 'react-icons/fa';
import UserImage from './UserImage';
import './Post.css';
import api from '../services/api';

export default class Post extends Component {
    state = {
        color: this.props.head == null ? '' : '#252122',
        marginLeft: this.props.head == null ? '40px' : '5px',
        type: this.props.head == null ? this.props.type : null,
        iLiked: false,
        nLikes: this.props.post.Likes.length,
        iDisliked: false,
        nDislikes: this.props.post.Dislikes.length,
        iFollow: false,
        user: {
            ID: this.props.post.Owner.Id,
            image: this.props.post.Owner.image,
            username: this.props.post.Owner.Nickname,
            name: this.props.post.Owner.Firstname,
            lastname: this.props.post.Owner.Lastname,
        },
    };
    componentDidMount = async () => {
        const { data: iFollow } = await api.get(
            `/follow?AccessToken=${this.props.token}`
        );
        iFollow.forEach(followed => {
            if (followed.UserId === this.state.user.ID) {
                this.setState({ iFollow: true });
            }
        });
        this.updateLikeDislike();
    };
    updateLikeDislike = () => {
        if (this.props.post.Likes.indexOf(this.props.user.ID) !== -1) {
            this.setState({
                iLiked: true,
            });
            console.log('like');
            console.log(this.props.post.Likes);
        }
        if (this.props.post.Dislikes.indexOf(this.props.user.ID) !== -1) {
            this.setState({
                iDisliked: true,
            });
            console.log('dislike');
            console.log(this.props.post.Dislikes);
        }
    };
    handleFollow = async () => {
        if (!this.state.iFollow) {
            await api.post(
                `/follow?AccessToken=${this.props.token}&RequestedUserId=${this.state.user.ID}`
            );
            this.setState({ iFollow: true });
        } else {
            await api.delete(
                `/follow?AccessToken=${this.props.token}&FollowedId=${this.state.user.ID}`
            );
            this.setState({ iFollow: false });
        }
    };
    handleLike = async () => {
        await api.put(
            `/posts/like?AccessToken=${this.props.token}&PostId=${this.props.post.Id}`
        );
        const nL = this.state.nLikes;
        const nD = this.state.nDislikes;
        if (this.state.iLiked) {
            this.setState({ iLiked: false, nLikes: nL - 1 });
        } else {
            this.setState({ iLiked: true, nLikes: nL + 1 });
        }
        if (this.state.iDisliked) {
            this.setState({ iDisliked: false, nDislikes: nD - 1 });
        }
    };
    handleDislike = async () => {
        await api.put(
            `/posts/dislike?AccessToken=${this.props.token}&PostId=${this.props.post.Id}`
        );
        const nL = this.state.nLikes;
        const nD = this.state.nDislikes;
        if (this.state.iDisliked) {
            this.setState({ iDisliked: false, nDislikes: nD - 1 });
        } else {
            this.setState({ iDisliked: true, nDislikes: nD + 1 });
        }
        if (this.state.iLiked) {
            this.setState({ iLiked: false, nLikes: nL - 1 });
        }
    };
    render() {
        return (
            <div
                id="post-container"
                style={{
                    background: this.state.color,
                    marginLeft: this.state.marginLeft,
                }}
            >
                <div id="head">
                    <div id="user-image">
                        <UserImage size="50px" image={this.state.user.image} />
                        {this.state.iFollow ? (
                            <FaTimes
                                id="follow-icon"
                                onClick={this.handleFollow}
                            />
                        ) : (
                            <FaPlus
                                id="follow-icon"
                                onClick={this.handleFollow}
                            />
                        )}
                    </div>
                    <div id="user-name">
                        <strong>{this.state.user.username}</strong>
                        <p id="name">
                            {this.state.user.name} {this.state.user.lastname}
                        </p>
                    </div>
                    <div id="comments">
                        <FaComments id="comments-icon" />
                    </div>
                </div>
                <div id="body">
                    <p id="comment">{this.props.post.Text}</p>
                    <img
                        id="image"
                        alt=""
                        src={
                            this.props.post.MediaObjects.length === 0
                                ? ''
                                : this.props.post.MediaObjects[0].URL
                        }
                    />
                </div>
                <div id="foot">
                    <p id="number">
                        <strong>{this.state.nDislikes}</strong>
                    </p>
                    {this.state.iDisliked ? (
                        <FaThumbsDown
                            onClick={this.handleDislike}
                            id="dislike-icon"
                        />
                    ) : (
                        <FaRegThumbsDown
                            onClick={this.handleDislike}
                            id="dislike-icon"
                        />
                    )}
                    <p id="number">
                        <strong>{this.state.nLikes}</strong>
                    </p>
                    {this.state.iLiked ? (
                        <FaThumbsUp onClick={this.handleLike} id="like-icon" />
                    ) : (
                        <FaRegThumbsUp
                            onClick={this.handleLike}
                            id="like-icon"
                        />
                    )}
                    <FaComment id="comment-icon" />
                    {this.state.user.is_product == null ? null : (
                        <FaCartPlus id="cart-icon" />
                    )}
                </div>
            </div>
        );
    }
}
