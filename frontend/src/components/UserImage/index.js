import React from 'react';
import { FaUserCircle } from 'react-icons/fa';
import { Container } from './styles';

export default function UserImage({ size, image }) {
    return (
        <Container size={size} image={image}>
            {image === null ? (
                <FaUserCircle id="user-icon" />
            ) : (
                <div id="user-image" />
            )}
        </Container>
    );
}
