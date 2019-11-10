import React, { Component } from 'react';
import './UserHead.css';
import api from '../../services/api';
import UserImage from '../UserImage';

export default class UserHead extends Component {
    render() {
        const { user } = this.props;
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
