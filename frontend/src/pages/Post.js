import React, { Component } from 'react';
import {
    FaRegThumbsUp,
    FaRegThumbsDown,
    FaCommentAlt,
    FaShoppingCart
} from 'react-icons/fa';
import UserImage from './UserImage';
import './Post.css';

export default class Post extends Component {
    state = {
        ID: this.props.postID
    };
    render() {
        return (
            <div id="post-container">
                <div id="left-post">
                    <div id="user-image">
                        <UserImage id="userImage"/>
                    </div>
                    <div id="user-name"><strong>Fulano</strong></div>
                    <div id="like-dislike">
                        <FaRegThumbsUp id="like-icon"/>
                        <FaRegThumbsDown id="dislike-icon"/>
                    </div>
                    {this.state.ID.is_product == null ?
                        null
                    :
                        <FaShoppingCart id="cart-icon"/>
                    }
                    <FaCommentAlt id="comment-icon"/>
                </div>
                <div id="right-post">
                    <textarea id="comment" readOnly="readOnly">
                        Lorem ipsum dolor sit amet,
                        consectetur adipiscing elit,
                        sed do eiusmod tempor incididunt
                        ut labore et dolore magna aliqua.
                        Ut enim ad minim veniam, quis
                        nostrud exercitation ullamco laboris
                        nisi ut aliquip ex ea commodo consequat
                    </textarea>
                    {this.state.ID.image == null ?
                        null
                    :
                        <image id="midia"></image>
                    }
                </div>
            </div>
        );
    }
}
