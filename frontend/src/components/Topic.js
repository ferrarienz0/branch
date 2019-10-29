import React, { Component } from 'react';
import { FaComment, FaPlus, FaTimes } from 'react-icons/fa';
import './Topic.css';
import api from '../services/api';

export default class Topic extends Component {
    state = {
        topicID: 0,
        followed: false,
    };
    handleFollow = async () => {
        await api
            .post(
                `/userInterests?AccessToken=${this.props.match.params.token}&SubjectId={this.state.topicID}`
            )
            .then(res => {
                this.setState({ followed: true });
            });
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
                        {this.state.followed ? (
                            <FaTimes id="plus" onClick={this.handleFollow} />
                        ) : (
                            <FaPlus id="plus" onClick={this.handleFollow} />
                        )}
                    </div>
                </div>
            </div>
        );
    }
}
