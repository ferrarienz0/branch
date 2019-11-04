import React, { Component } from 'react';
import {
    FaThumbsUp,
    FaThumbsDown,
    FaRegThumbsUp,
    FaRegThumbsDown,
    FaComment,
    FaArrowUp,
    FaCartPlus,
    FaPlus,
    FaTimes,
} from 'react-icons/fa';
import './Comment.css';
import UserImage from './UserImage';
import api from '../services/api';

export default class Comment extends Component {
    state = {
        iLiked: false,
        nLikes: this.props.comment.Likes.length,
        iDisliked: false,
        nDislikes: this.props.comment.Dislikes.length,
        iFollow: false,
    };

    componentDidMount = async () => {
        const { token, me } = this.props;
        const { data: iFollow } = await api.get(`/follow?AccessToken=${token}`);
        console.log(iFollow);
        iFollow.forEach(followed => {
            if (followed.UserId === me.ID) {
                this.setState({ iFollow: true });
            }
        });
        if (this.props.comment.Likes.indexOf(me.ID) !== -1) {
            this.setState({
                iLiked: true,
            });
        }
        if (this.props.comment.Dislikes.indexOf(me.ID) !== -1) {
            this.setState({
                iDisliked: true,
            });
        }
    };

    handleLike = async () => {
        const { data } = await api.put(
            `/posts/like?AccessToken=${this.props.token}&PostId=${this.props.comment.Id}`
        );
        this.setState({
            nLikes: data.TotalLikes,
            nDislikes: data.TotalDeslikes,
        });
        if (this.state.iLiked) this.setState({ iLiked: false });
        else this.setState({ iLiked: true, iDisliked: false });
    };

    handleDislike = async () => {
        const { data } = await api.put(
            `/posts/dislike?AccessToken=${this.props.token}&PostId=${this.props.comment.Id}`
        );
        this.setState({
            nLikes: data.TotalLikes,
            nDislikes: data.TotalDeslikes,
        });
        if (this.state.iDisliked) this.setState({ iDisliked: false });
        else this.setState({ iDisliked: true, iLiked: false });
    };

    handleFollow = () => {
        const { iFollow } = this.state;
        const { comment, token } = this.props;
        if (!iFollow) {
            api.post(
                `/follow?AccessToken=${token}&RequestedUserId=${comment.Owner.Id}`
            ).then(() => this.setState({ iFollow: true }));
        } else {
            api.delete(
                `/follow?AccessToken=${token}&FollowedId=${comment.Owner.Id}`
            ).then(() => this.setState({ iFollow: false }));
        }
    };

    render() {
        const { comment, handleHead } = this.props;
        const { iLiked, nLikes, iDisliked, nDislikes, iFollow } = this.state;
        return (
            <div id="comment-container">
                <div id="head">
                    <div id="user-image">
                        <UserImage size="50px" image={comment.Owner.image} />
                        {iFollow ? (
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
                        <strong>@{comment.Owner.Nickname}</strong>
                        <p id="name">
                            {comment.Owner.Firstname} {comment.Owner.Lastname}
                        </p>
                    </div>
                    <div id="go-ahead">
                        <FaArrowUp
                            id="go-ahead-icon"
                            onClick={() => handleHead('c', comment.Id)}
                        />
                    </div>
                </div>
                <div id="body">
                    <p id="text">{comment.Text}</p>
                    <img
                        id="image"
                        alt=""
                        src={
                            comment.MediaObjects.length === 0
                                ? ''
                                : comment.MediaObjects[0].URL
                        }
                    />
                </div>
                <div id="foot">
                    <p id="number">
                        <strong>{nDislikes}</strong>
                    </p>
                    {iDisliked ? (
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
                        <strong>{nLikes}</strong>
                    </p>
                    {iLiked ? (
                        <FaThumbsUp onClick={this.handleLike} id="like-icon" />
                    ) : (
                        <FaRegThumbsUp
                            onClick={this.handleLike}
                            id="like-icon"
                        />
                    )}
                    <FaComment id="comment-icon" />
                    {this.props.me.is_product == null ? null : (
                        <FaCartPlus id="cart-icon" />
                    )}
                </div>
            </div>
        );
    }
}
