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
import './Topic.css';

export default class Topic extends Component {
    state = {
        ID: this.props.postID,
    };
    render() {
        return (
            <div id="topic-container">
                <p id="hashtag">#Topico</p>
                <div />
            </div>
        );
    }
}
