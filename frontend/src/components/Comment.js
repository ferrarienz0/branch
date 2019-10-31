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
        iDisliked: false,
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

    handleLike = async () => {
        await api.put(
            `/posts/like?AccessToken=${this.props.token}&PostId=${this.props.comment.Id}`
        );
        this.props.refresh();
    };

    handleDislike = async () => {
        await api.put(
            `/posts/dislike?AccessToken=${this.props.token}&PostId=${this.props.comment.Id}`
        );
        this.props.refresh();
    };

    render() {
        return (
            <div id="comment-container">
                <div id="head">
                    <div id="user-image">
                        <UserImage size="50px" image={this.props.me.image} />
                        {false ? (
                            <FaTimes id="follow-icon" />
                        ) : (
                            <FaPlus id="follow-icon" />
                        )}
                    </div>
                    <div id="user-name">
                        <strong>@{this.props.comment.Owner.Nickname}</strong>
                        <p id="name">
                            {this.props.comment.Owner.Firstname}{' '}
                            {this.props.comment.Owner.Lastname}
                        </p>
                    </div>
                    <div id="comments">
                        <FaArrowUp id="comments-icon" />
                    </div>
                </div>
                <div id="body">
                    <p id="text">{this.props.comment.Text}</p>
                    <img
                        id="image"
                        alt=""
                        src={
                            this.props.comment.MediaObjects.length === 0
                                ? ''
                                : this.props.comment.MediaObjects[0].URL
                        }
                    />
                </div>
                <div id="foot">
                    <p id="number">
                        <strong>{this.props.comment.Dislikes.length}</strong>
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
                        <strong>{this.props.comment.Likes.length}</strong>
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
                    {this.props.me.is_product == null ? null : (
                        <FaCartPlus id="cart-icon" />
                    )}
                </div>
            </div>
        );
    }
}
