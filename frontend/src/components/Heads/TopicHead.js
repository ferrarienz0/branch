import React, { Component } from 'react';
import './TopicHead.css';
import { FaComment, FaPlus, FaTimes } from 'react-icons/fa';
import api from '../../services/api';

export default class TopicHead extends Component {
    state = {
        topic: {},
        wallpaper: '',
        posting: false,
        followID: '',
        followed: false,
        reload: false,
    };

    componentDidMount = async () => {
        const { data: topic } = await api.get(
            `/subject?id=${this.props.topicID}`
        );
        this.setState({ topic, wallpaper: topic.Media.URL });
        const { data: myTopics } = await api.get(
            `/userInterests?AccessToken=${this.props.token}`
        );
        myTopics.forEach(follow => {
            if (follow.Subject.Id === this.props.topicID) {
                this.setState({
                    followID: follow.UserInterestId,
                    followed: true,
                });
            }
        });
    };

    handleFollow = () => {
        if (!this.state.followed) {
            api.post(
                `/userInterests?AccessToken=${this.props.token}&SubjectId=${this.props.topicID}`
            ).then(res => {
                this.setState({ followId: res.data.Id, followed: true });
            });
        } else {
            api.delete(`/userInterests?id=${this.state.followID}`).then(res => {
                this.setState({ followed: false });
            });
        }
    };

    render() {
        return (
            <div
                id="topichead-container"
                style={{
                    backgroundImage: `linear-gradient(
                    135deg,
                    var(--black) 25%,
                    transparent 110%
                ),  url(${this.state.wallpaper})`,
                    backgroundPosition: 'center',
                    backgroundRepeat: 'no-repeat',
                    backgroundSize: 'cover',
                }}
            >
                <h2 id="hashtag">{this.state.topic.Hashtag}</h2>
                <div id="foot">
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
            </div>
        );
    }
}
