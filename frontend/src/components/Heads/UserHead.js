import React, { Component } from 'react';
import './UserHead.css';
import api from '../../services/api';
import UserImage from '../UserImage';

export default class UserHead extends Component {
    state = {
        user: {},
    };
    componentDidMount = async () => {
        const { data: user } = await api.get(`/user?id=${this.props.userID}`);
        console.log(user);
        this.setState({ user });
    };
    render() {
        const { user } = this.state;
        return (
            <div id="userhead-container">
                <UserImage size="70px" />
                <div id="text">
                    <p id="username">
                        <strong>@{user.Nickname}</strong>
                    </p>
                    <p id="name">
                        {user.Firstname} {user.Lastname}
                    </p>
                </div>
            </div>
        );
    }
}
