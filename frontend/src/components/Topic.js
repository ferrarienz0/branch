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
                className="topic-container"
                style={{
                    backgroundImage: `url(${this.props.wallpaper})`,
                    backgroundPosition: 'center',
                    backgroundRepeat: 'no-repeat',
                    backgroundSize: 'cover',
                }}
            >
                <div className="gradient">
                    <h2 className="hashtag">#{this.props.hashtag}</h2>
                    <div className="foot">
                        <FaComment className="comment" />
                        {this.state.followed ? (
                            <FaTimes
                                className="plus"
                                onClick={this.handleFollow}
                            />
                        ) : (
                            <FaPlus
                                className="plus"
                                onClick={this.handleFollow}
                            />
                        )}
                    </div>
                </div>
            </div>
        );
    }
}
