import React, { Component } from 'react';
import {
    FaRegThumbsUp,
    FaThumbsUp,
    FaRegThumbsDown,
    FaThumbsDown,
    FaComment,
    FaComments,
    FaCartPlus,
} from 'react-icons/fa';
import UserImage from './UserImage';
import './Post.css';

export default class Post extends Component {
    state = {
        ID: this.props.postID,
    };
    render() {
        return (
            <div id="post-container">
                <div id="head">
                    <div id="user-image">
                        <UserImage />
                    </div>
                    <div id="user-name">
                        <strong>@Lorem_Ipsum</strong>
                    </div>
                    <div id="comments">
                        <FaComments id="comments-icon" />
                    </div>
                </div>
                <div id="body">
                    <p id="comment">
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                        sed do eiusmod tempor incididunt ut labore et dolore
                        magna aliqua. Ut enim ad minim veniam, quis nostrud
                        exercitation ullamco laboris nisi ut aliquip ex ea
                        commodo consequat
                    </p>
                    {this.state.ID.image == null ? null : (
                        <image id="midia"></image>
                    )}
                </div>
                <div id="foot">
                    <FaRegThumbsDown id="dislike-icon" />
                    <FaRegThumbsUp id="like-icon" />
                    <FaComment id="comment-icon" />
                    {this.state.ID.is_product == null ? (
                        <FaCartPlus id="cart-icon" />
                    ) : (
                        <FaCartPlus id="cart-icon" />
                    )}
                </div>
                <div id="right-post"></div>
            </div>
        );
    }
}
