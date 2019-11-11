import React, { Component } from 'react';
import './TopicHead.css';
import { FaComment, FaPlus, FaTimes } from 'react-icons/fa';
import api from '../../services/api';

export default class TopicHead extends Component {
    state = {
        followed: false,
        posting: false,
        reload: false,
    };

    componentDidMount = async () => {
        //get tópicos que logado segue
        const { data: topics } = await api.get(
            `/subjects/followed?AccessToken=${this.props.token}`
        );
        //verifica se este tópico é seguido por logado
        console.log(this.props.topic.ID);
        topics.forEach(follow => {
            console.log(follow);
            if (follow.Id === this.props.topic.ID) {
                this.setState({
                    followed: true,
                });
            }
        });
    };

    handleFollow = () => {
        const { followed } = this.state;
        const { token, topic } = this.props;
        if (!followed) {
            api.post(
                `/user/follow/subject?AccessToken=${token}&SubjectId=${topic.ID}`
            ).then(res => {
                this.setState({ followed: true });
            });
        } else {
            api.delete(
                `/user/unfollow/subject?SubjectFollowId=${topic.ID}`
            ).then(res => {
                this.setState({ followed: false });
            });
        }
    };

    render() {
        const { topic } = this.props;
        return (
            <div
                id="topichead-container"
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
