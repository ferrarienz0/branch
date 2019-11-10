import React, { Component } from 'react';
import '../pages/Home.css';
import Topic from '../components/Topic';
import UserImage from '../components/UserImage';

export default class Follows extends Component {
    render() {
        const { me, topics, handleHead, token } = this.props;
        return (
            <div id="follows">
                <div id="users">Quem eu sigo</div>
                {me.users.map((user, index) => (
                    <div
                        id="user"
                        onClick={() => handleHead('user', user.Id)}
                        key={index}
                    >
                        <UserImage size="30px" image={user.Media} />
                        <div id="name">@{user.Nickname}</div>
                    </div>
                ))}
                <div id="topics">Meus tópicos</div>
                {me.topics.map((topic, index) => (
                    <Topic
                        key={index}
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
                        token={token}
                        topic={{
                            id: topic.Id,
                            hashtag: topic.Hashtag,
                            banner: topic.Media.URL,
                            follow: false,
                        }}
                        onHead={() => handleHead('topic', topic.Id)}
                    />
                ))}
            </div>
        );
    }
}
