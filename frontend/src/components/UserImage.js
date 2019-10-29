import React from 'react';
import { FaUserCircle } from 'react-icons/fa';
//import Perfil from './Perfil';
import './UserImage.css';

//componente responsável por exibir o ícone do usuário
export default function UserImage(props) {
    return (
        <div id="userimage-container">
            {props.image == null ? (
                <FaUserCircle
                    id="user-icon"
                    style={{
                        height: `${props.size}`,
                        width: `${props.size}`,
                        background: `url('${props.image}')`,
                        backgroundPosition: 'center',
                        backgroundRepeat: 'no-repeat',
                        backgroundSize: 'cover',
                    }}
                />
            ) : (
                <div
                    id="user-image"
                    style={{
                        height: `${props.size}`,
                        width: `${props.size}`,
                        background: `url('${props.image}')`,
                        backgroundPosition: 'center',
                        backgroundRepeat: 'no-repeat',
                        backgroundSize: 'cover',
                    }}
                />
            )}
            {/*<Perfil />*/}
        </div>
    );
}
