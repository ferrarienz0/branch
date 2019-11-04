import React, { Component } from 'react';
import {
    FaThumbsUp,
    FaThumbsDown,
    FaRegThumbsUp,
    FaRegThumbsDown,
    FaComment,
    FaCartPlus,
    FaPlus,
    FaTimes,
} from 'react-icons/fa';
import './CommentHead.css';
import UserImage from '../UserImage';
import api from '../../services/api';

export default class CommentHead extends Component {
    state = {
        iLiked: false,
        nLikes: this.props.comment.Likes.length,
        iDisliked: false,
        nDislikes: this.props.comment.Dislikes.length,
    };

    handleLike = async () => {
        const { token, comment } = this.props;
        const { data } = await api.put(
            `/posts/like?AccessToken=${token}&PostId=${comment.Id}`
        );
        this.setState({
            nLikes: data.TotalLikes,
            nDislikes: data.TotalDeslikes,
        });
        if (this.state.iLiked) this.setState({ iLiked: false });
        this.setState({ iLiked: true, iDisliked: false });
    };

    handleDislike = async () => {
        const { token, comment } = this.props;
        const { data } = await api.put(
            `/posts/dislike?AccessToken=${token}&PostId=${comment.Id}`
        );
        this.setState({
            nLikes: data.TotalLikes,
            nDislikes: data.TotalDeslikes,
        });
        if (this.state.iDisliked) this.setState({ iDisliked: false });
        this.setState({ iDisliked: true, iLiked: false });
    };

    render() {
        const { comment } = this.props;
        const { iDisliked, iLiked } = this.state;
        return (
            <div id="commenthead-container">
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
                        <strong>{comment.Dislikes.length}</strong>
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
                        <strong>{comment.Likes.length}</strong>
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
