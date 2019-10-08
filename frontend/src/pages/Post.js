import React, { Component } from 'react';
import {
    FaRegThumbsUp,
    FaRegThumbsDown,
} from 'react-icons/fa';
import UserImage from './UserImage';
import './Post.css';

export default class Post extends Component {
    state = {
        ID: this.props.postID
    };
    render() {
        return (
            <div className="post-container">
                <div className="userImageDiv">
                    <UserImage className="userImage"/>
                </div>
            </div>
        );
    }
}
