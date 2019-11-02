import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { FaComment, FaPlus, FaTimes, FaArrowUp } from 'react-icons/fa';
import './Topic.css';
import api from '../services/api';
import Posting from '../components/Posting';

export default class Topic extends Component {
    state = {
        topicID: this.props.topicId,
        followId: this.props.followId,
        followed: this.props.followed,
        posting: false,
        reload: false,
    };
    handleFollow = () => {
        if (!this.state.followed) {
            api.post(
                `/userInterests?AccessToken=${this.props.token}&SubjectId=${this.state.topicID}`
            ).then(res => {
                this.setState({ followId: res.data.Id, followed: true });
                this.props.refresh();
            });
        } else {
            api.delete(`/userInterests?id=${this.state.followId}`).then(res => {
                this.setState({ followed: false });
                this.props.refresh();
            });
        }
    };
    handleComment = async () => {};
    render() {
        if (this.state.reload) {
            this.setState({ reload: false });
            return (
                <Redirect
                    to={`/home/${this.props.token}/h/${this.state.topicID}`}
                />
            );
        }
        return (
            <div
                id="topic-container"
                style={{
                    backgroundImage: `linear-gradient(
                    135deg,
                    var(--black) 25%,
                    transparent 110%
                ),  url(${this.props.wallpaper})`,
                    backgroundPosition: 'center',
                    backgroundRepeat: 'no-repeat',
                    backgroundSize: 'cover',
                }}
            >
                <h2 id="hashtag">{this.props.hashtag}</h2>
                <div id="foot">
                    <FaArrowUp
                        id="go-ahead-icon"
                        onClick={() =>
                            this.setState({
                                reload: true,
                            })
                        }
                    />
                    <FaComment
                        id="comment"
                        onClick={e =>
                            this.state.posting
                                ? this.setState({ posting: false })
                                : this.setState({ posting: true })
                        }
                    />
                    {this.state.followed ? (
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
