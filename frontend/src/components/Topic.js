import React, { Component } from 'react';
import { FaComment, FaPlus, FaTimes } from 'react-icons/fa';
import './Topic.css';
import api from '../services/api';
import Posting from '../components/Posting';

export default class Topic extends Component {
    state = {
        topicID: this.props.topicId,
        followId: this.props.followId,
        followed: this.props.followed,
        posting: false,
    };
    handleFollow = () => {
        if (!this.state.followed) {
            api.post(
                `/userInterests?AccessToken=${this.props.token}&SubjectId=${this.state.topicID}`
            ).then(res => {
                this.setState({ followId: res.data.Id, followed: true });
            });
        } else {
            api.delete(`/userInterests?id=${this.state.followId}`).then(res => {
                this.setState({ followed: false });
            });
        }
    };
    handleComment = async () => {};
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
                    <h2 className="hashtag">{this.props.hashtag}</h2>
                    <div className="foot">
                        <FaComment
                            className="comment"
                            onClick={e =>
                                this.state.posting
                                    ? this.setState({ posting: false })
                                    : this.setState({ posting: true })
                            }
                        />
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
