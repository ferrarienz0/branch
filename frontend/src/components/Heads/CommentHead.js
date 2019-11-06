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
import './CommentHead.css';
import UserImage from '../UserImage';
import api from '../../services/api';

export default class CommentHead extends Component {
    state = {
        comment: { Owner: {}, MediaObjects: [], Dislikes: [], Likes: [] },
    };
    componentDidMount = async () => {
        const { data: comment } = await api.get(
            `/posts?PostId=${this.props.commentID}`
        );
        this.setState({ comment });
    };
    render() {
        return (
            <div id="commenthead-container">
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
                        <strong>@{this.state.comment.Owner.Nickname}</strong>
                        <p id="name">
                            {this.state.comment.Owner.Firstname}{' '}
                            {this.state.comment.Owner.Lastname}
                        </p>
                    </div>
                </div>
                <div id="body">
                    <p id="text">{this.state.comment.Text}</p>
                    <img
                        id="image"
                        alt=""
                        src={
                            this.state.comment.MediaObjects.length === 0
                                ? ''
                                : this.state.comment.MediaObjects[0].URL
                        }
                    />
                </div>
                <div id="foot">
                    <p id="number">
                        <strong>{this.state.comment.Dislikes.length}</strong>
                    </p>
                    {true ? (
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
                        <strong>{this.state.comment.Likes.length}</strong>
                    </p>
                    {false ? (
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
