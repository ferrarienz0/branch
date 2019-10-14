import React, { Component } from 'react';
import {
    FaThumbsUp,
    FaThumbsDown,
    FaRegThumbsUp,
    FaRegThumbsDown,
    FaComment,
    FaComments,
    FaCartPlus,
    FaUserPlus,
} from 'react-icons/fa';
import UserImage from './UserImage';
import './Post.css';

export default class Post extends Component {
    state = {
        color: this.props.head == null ? '' : '#252122',
        marginLeft: this.props.head == null ? '40px' : '5px',
        ID: this.props.user.id,
        like: false,
        dislike: false,
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
                        <UserImage size="50px" image={this.props.user.image} />
                        <FaUserPlus id="follow-icon" />
                    </div>
                    <div id="user-name">
                        <strong>{this.props.user.username}</strong>
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
                    {this.state.dislike ? (
                        <FaThumbsDown
                            onClick={() => this.setState({ dislike: false })}
                            id="dislike-icon"
                        />
                    ) : (
                        <FaRegThumbsDown
                            onClick={() => this.setState({ dislike: true })}
                            id="dislike-icon"
                        />
                    )}
                    {this.state.like ? (
                        <FaThumbsUp
                            onClick={() => this.setState({ like: false })}
                            id="like-icon"
                        />
                    ) : (
                        <FaRegThumbsUp
                            onClick={() => this.setState({ like: true })}
                            id="like-icon"
                        />
                    )}
                    <FaComment id="comment-icon" />
                    {this.state.ID.is_product == null ? (
                        <FaCartPlus id="cart-icon" />
                    ) : (
                        <FaCartPlus id="cart-icon" />
                    )}
                </div>
            </div>
        );
    }
}
