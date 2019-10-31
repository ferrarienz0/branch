import React, { Component } from 'react';
import './Head.css';
import UserImage from '../components/UserImage';

export default class Head extends Component {
    topic = () => {};
    user = () => {
        return (
            <div id="me">
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
    };
    product = () => {};
    me = () => {
        return (
            <div id="me">
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
    };
    render() {
        return (
            <div id="head-container">
                {this.props.type === '#'
                    ? this.topic()
                    : this.props.type === '@'
                    ? this.user()
                    : this.props.type === '$'
                    ? this.product()
                    : this.me()}
            </div>
        );
    }
}
