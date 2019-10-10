import React from 'react';
import { FaUserCircle } from 'react-icons/fa';
import './UserImage.css';

export default function UserImage(props) {
    const user = props.user;
    return (
        <div id="userimage-container">
            {user == null ? (
                <FaUserCircle id="user-icon" />
            ) : (
                <img src={user.image} />
            )}
        </div>
    );
}
