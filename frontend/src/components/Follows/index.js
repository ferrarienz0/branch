import React, { Component } from 'react';
import { Container, User } from './styles';
import Topic from '../../components/Topic';
import UserImage from '../../components/UserImage';

export default class Follows extends Component {
    render() {
        const { me, topics, handleHead, token } = this.props;
        return (
            <Container>
                <div id="users">Quem eu sigo</div>
                {me.users.map((user, index) => (
                    <User
                        onClick={() => handleHead('user', user.Id)}
                        key={index}
                    >
                        <UserImage size="30px" image={user.Media} />
                        <div id="name">@{user.Nickname}</div>
                    </User>
                ))}
                <div id="topics">Meus tópicos</div>
                {me.topics.map((topic, index) => (
                    <Topic
                        key={index}
                        head={false}
                        token={token}
                        topic={{
                            id: topic.Id,
                            hashtag: topic.Hashtag,
                            banner: topic.Media.URL,
                            follow: true,
                        }}
                        onHead={() => handleHead('topic', topic.Id)}
                    />
                ))}
                <div id="topics">Recomendado</div>
                {topics.map((topic, index) => (
                    <Topic
                        key={index}
                        head={false}
                        token={token}
                        topic={{
                            id: topic.Id,
                            hashtag: topic.Hashtag,
                            banner:
                                topic.Media === null ? null : topic.Media.URL,
                            follow: false,
                        }}
                        onHead={() => handleHead('topic', topic.Id)}
                    />
                ))}
            </Container>
        );
    }
}
