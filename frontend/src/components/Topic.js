import React, { Component } from 'react';
import { FaComment, FaPlus } from 'react-icons/fa';
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
                    backgroundPosition: 'center',
                    backgroundRepeat: 'no-repeat',
                    backgroundSize: 'cover',
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
