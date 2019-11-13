import React, { Component } from 'react';
import './UserHead.css';
import { FaTimes, FaPlus } from 'react-icons/fa';
import api from '../../services/api';
import UserImage from '../UserImage';

export default class UserHead extends Component {
    state = {
        iFollow: false,
    };

    componentDidMount = async () => {
        const { token, user } = this.props;
        const { data: iFollow } = await api.get(
            `/follows?AccessToken=${token}`
        );
        iFollow.forEach(followed => {
            if (followed.Id === user.Id) {
                this.setState({ iFollow: true });
            }
        });
    };

    handleFollow = () => {
        const { iFollow } = this.state;
        const { user, token } = this.props;
        if (!iFollow) {
            api.post(
                `/follow/create?AccessToken=${token}&RequestedUserId=${user.Id}`
            ).then(res => {
                this.setState({ iFollow: true });
            });
        } else {
            api.delete(
                `/follow/delete?AccessToken=${token}&FollowedId=${user.Id}`
            ).then(res => {
                this.setState({ iFollow: false });
            });
        }
    };

    render() {
        const { iFollow } = this.state;
        const { user, me } = this.props;
        return (
            <div id="userhead-container">
                <div id="user-image">
                    <UserImage size="70px" />
                    {me.ID === user.Id ? null : iFollow ? (
                        <FaTimes id="follow-icon" onClick={this.handleFollow} />
                    ) : (
                        <FaPlus id="follow-icon" onClick={this.handleFollow} />
                    )}
                </div>
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
