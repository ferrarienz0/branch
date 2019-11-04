import React, { Component } from 'react';
import '../pages/Home.css';
import api from '../services/api';
import Topic from '../components/Topic';

export default class Follows extends Component {
    state = {
        user: {
            topics: [],
        },
        topics: [],
    };

    componentDidMount = async () => {
        const { token } = this.props;
        const { data: myTopics } = await api.get(
            `/userInterests?AccessToken=${token}`
        );
        const { data: topics } = await api.get(`/subject?AccessToken=${token}`);
        this.setState({
            user: {
                topics: myTopics,
            },
            topics,
        });
    };

    refresh = () => {
        window.location.reload();
    };

    render() {
        const { user, topics } = this.state;
        const { token, handleHead } = this.props;
        return (
            <div id="follows">
                <div id="topics">Meus tópicos</div>
                {user.topics.map((topic, index) => (
                    <Topic
                        key={index}
                        token={token}
                        topic={{
                            id: topic.Subject.Id,
                            hashtag: topic.Subject.Hashtag,
                            followID: topic.UserInterestId,
                            banner: topic.Subject.Media.URL,
                        }}
                        refresh={this.refresh}
                        handleHead={handleHead}
                    />
                ))}
                <div id="topics">Recomendado</div>
                {topics.map((topic, index) => (
                    <Topic
                        key={index}
                        token={token}
                        topic={{
                            id: topic.Id,
                            hashtag: topic.Hashtag,
                            banner: topic.Media.URL,
                        }}
                        refresh={this.refresh}
                        handleHead={handleHead}
                    />
                ))}
            </div>
        );
    }
}
