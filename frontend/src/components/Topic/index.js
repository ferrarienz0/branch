import React, { Component } from 'react';
import { FaComment, FaPlus, FaTimes, FaArrowUp } from 'react-icons/fa';
import { Container, Footer } from './styles';
import api from '../../services/api';
import Posting from '../../components/Posting';

export default class Topic extends Component {
    state = {
        follow: this.props.topic.follow,
        posting: false,
    };

    componentDidMount = async () => {
        const { topic, token } = this.props;
        if (topic.follow === undefined) {
            const { data: topics } = await api.get(
                `/subjects/followed?AccessToken=${token}`
            );
            topics.forEach(follow => {
                if (follow.Id === topic.ID) {
                    this.setState({
                        follow: true,
                    });
                }
            });
        }
    };

    Follow = () => {
        const { token, topic } = this.props;
        api.post(
            `/user/follow/subject?AccessToken=${token}&SubjectId=${topic.id}`
        ).then(() => {
            this.setState({ follow: true });
        });
    };

    Unfollow = () => {
        const { token, topic } = this.props;
        api.delete(
            `/user/unfollow/subject?AccessToken=${token}&SubjectId=${topic.id}`
        ).then(() => {
            this.setState({ follow: false });
        });
    };

    render() {
        const { posting, follow } = this.state;
        const { head, topic, token, onHead } = this.props;
        return (
            <Container banner={topic.banner} head={head}>
                <h2>{topic.hashtag}</h2>
                <Footer>
                    {head ? null : (
                        <FaArrowUp id="go-ahead-icon" onClick={onHead} />
                    )}
                    <FaComment
                        id="comment-icon"
                        onClick={e =>
                            posting
                                ? this.setState({ posting: false })
                                : this.setState({ posting: true })
                        }
                    />
                    {follow ? (
                        <FaTimes id="follow-icon" onClick={this.Unfollow} />
                    ) : (
                        <FaPlus id="follow-icon" onClick={this.Follow} />
                    )}
                </Footer>
                {posting ? (
                    <Posting
                        token={token}
                        onClose={() => this.setState({ posting: false })}
                    />
                ) : null}
            </Container>
        );
    }
}
