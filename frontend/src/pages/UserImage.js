import React from 'react';
import { FaUserCircle } from 'react-icons/fa';
import './UserImage.css';

export default function UserImage( props ) {
    const user = props.user;
    return (
        <div className="userimage-container">
            {user == null ? (
                <FaUserCircle className="userIcon" />
            ) : (
                <img src={user.image} />
            )}
        </div>
    );
}
