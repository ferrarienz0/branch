import React from 'react';
import { FaUserCircle } from 'react-icons/fa';
import './HeadPost.css';
import UserImage from './UserImage';

export default function HeadPost(props) {
    return (
        <div id="headpost-container">
            <div id="image">
                <UserImage />
            </div>
            <p id="name">
                <strong>@Lorem_Ipsum</strong>
            </p>
        </div>
    );
}
