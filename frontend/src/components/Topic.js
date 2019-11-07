import React, { Component } from 'react';
import { FaComment, FaPlus, FaTimes, FaArrowUp } from 'react-icons/fa';
import './Topic.css';
import api from '../services/api';
import Posting from '../components/Posting';

export default class Topic extends Component {
    state = {
        followID: this.props.topic.followID,
        posting: false,
        reload: false,
    };

    handleFollow = async () => {
        const { followID } = this.state;
        const { token, topic } = this.props;
        if (!Boolean(followID)) {
            const { data } = await api.post(
                `/userInterests?AccessToken=${token}&SubjectId=${topic.id}`
            );
            this.setState({ followID: data.Id });
            //this.props.refresh();
        } else {
            api.delete(`/userInterests?id=${followID}`).then(() => {
                this.setState({ followID: null });
                //this.props.refresh();
            });
        }
    };

    render() {
        const { posting, followID } = this.state;
        const { topic, handleHead } = this.props;
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
                    <FaArrowUp
                        id="go-ahead-icon"
                        onClick={() => handleHead('h', topic.id)}
                    />
                    <FaComment
                        id="comment"
                        onClick={e =>
                            posting
                                ? this.setState({ posting: false })
                                : this.setState({ posting: true })
                        }
                    />
                    {Boolean(followID) ? (
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
