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
        const { topic, token, onHead } = this.props;
        return (
            <Container banner={topic.banner}>
                <h2>{topic.hashtag}</h2>
                <Footer>
                    <FaArrowUp id="go-ahead-icon" onClick={onHead} />
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
