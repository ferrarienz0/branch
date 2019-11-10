import React, { Component } from 'react';
import { FaComment, FaPlus, FaTimes, FaArrowUp } from 'react-icons/fa';
import './Topic.css';
import api from '../services/api';
import Posting from '../components/Posting';

export default class Topic extends Component {
    state = {
        follow: this.props.topic.follow,
        posting: false,
        reload: false,
    };

    handleFollow = async () => {
        const { follow } = this.state;
        const { token, topic } = this.props;
        if (!follow) {
            api.post(
                `/user/follow/subject?AccessToken=${token}&SubjectId=${topic.id}`
            ).then(() => {
                this.setState({ follow: true });
            });
        } else {
            api.delete(
                `/user/unfollow/subject?AccessToken=${token}&SubjectId=${topic.id}`
            ).then(() => {
                this.setState({ follow: false });
            });
        }
    };

    render() {
        const { posting, follow } = this.state;
        const { topic, onHead } = this.props;
        return (
            <div
                id="topic-container"
                style={{
                    backgroundImage: `linear-gradient(
                    135deg,
                    var(--black) 25%,
                    transparent 110%
                ),  url(${topic.banner})`,
                    backgroundPosition: 'center',
                    backgroundRepeat: 'no-repeat',
                    backgroundSize: 'cover',
                }}
            >
                <h2 id="hashtag">{topic.hashtag}</h2>
                <div id="foot">
                    <FaArrowUp id="go-ahead-icon" onClick={onHead} />
                    <FaComment
                        id="comment"
                        onClick={e =>
                            posting
                                ? this.setState({ posting: false })
                                : this.setState({ posting: true })
                        }
                    />
                    {follow ? (
                        <FaTimes id="follow-icon" onClick={this.handleFollow} />
                    ) : (
                        <FaPlus id="follow-icon" onClick={this.handleFollow} />
                    )}
                </div>
                {this.state.posting ? (
                    <Posting
                        token={this.props.token}
                        onClose={() => this.setState({ posting: false })}
                    />
                ) : null}
            </div>
        );
    }
}
