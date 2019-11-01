import React, { Component } from 'react';
import './UserHead.css';
import UserImage from '../UserImage';

export default class UserHead extends Component {
    render() {
        return (
            <div id="userhead-container">
                <UserImage size="70px" />
                <div id="text">
                    <p id="username">
                        <strong>@{this.props.me.username}</strong>
                    </p>
                    <p id="name">
                        {this.props.me.name} {this.props.me.lastname}
                    </p>
                </div>
            </div>
        );
    }
}
