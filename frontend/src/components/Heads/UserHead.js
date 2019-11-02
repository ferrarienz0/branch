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
        this.setState({ user });
    };
    render() {
        return (
            <div id="userhead-container">
                <UserImage size="70px" />
                <div id="text">
                    <p id="username">
                        <strong>@{this.state.user.Nickname}</strong>
                    </p>
                    <p id="name">
                        {this.state.user.Firstname} {this.state.user.Lastname}
                    </p>
                </div>
            </div>
        );
    }
}
