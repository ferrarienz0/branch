import React, { Component } from 'react';
import {
    FaRegThumbsUp,
    FaThumbsUp,
    FaRegThumbsDown,
    FaThumbsDown,
    FaComment,
    FaComments,
    FaPlus,
} from 'react-icons/fa';
import './Topic.css';

export default class Topic extends Component {
    state = {
        ID: this.props.postID,
    };
    render() {
        return (
            <div
                id="topic-container"
                style={{
                    background: `url('${this.props.wallpaper}')`,
                    'background-position': 'center',
                    'background-repeat': 'no-repeat',
                    'background-size': 'cover',
                }}
            >
                <div id="gradient">
                    <h2 id="hashtag">#{this.props.hashtag}</h2>
                    <div id="foot">
                        <FaComment id="comment" />
                        <FaPlus id="plus" />
                    </div>
                </div>
            </div>
        );
    }
}
