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
        goAhead: false,
    };
    componentDidMount = async () => {
        const { data: iFollow } = await api.get(
            `/follow?AccessToken=${this.props.token}`
        );
        iFollow.forEach(followed => {
            if (followed.UserId === this.state.me.ID) {
                this.setState({ iFollow: true });
            }
        });
        if (this.props.comment.Likes.indexOf(this.props.me.ID) !== -1) {
            this.setState({
                iLiked: true,
            });
        }
        if (this.props.comment.Dislikes.indexOf(this.props.me.ID) !== -1) {
            this.setState({
                iDisliked: true,
            });
        }
    };

    handleGoahead = () => {
        this.setState({ goAhead: true });
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
        this.setState({ iLiked: true, iDisliked: false });
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
        this.setState({ iDisliked: true, iLiked: false });
    };

    render() {
        const { comment, handleHead } = this.props;
        const { iLiked, nLikes, iDisliked, nDislikes } = this.state;
        return (
            <div id="comment-container">
                <div id="head">
                    <div id="user-image">
                        <UserImage size="50px" image={comment.Owner.image} />
                        {false ? (
                            <FaTimes id="follow-icon" />
                        ) : (
                            <FaPlus id="follow-icon" />
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
