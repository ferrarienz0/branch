import React, { setState } from 'react';
import { FaUserCircle } from 'react-icons/fa';
import Perfil from './Perfil';
import './UserImage.css';

//componente responsável por exibir o ícone do usuário
export default function UserImage(props) {
    const [perfil, setPerfil] = setState(<div />);
    return (
        <div id="userimage-container">
            {props.image == null ? (
                <FaUserCircle id="user-icon" />
            ) : (
                <div
                    id="user-image"
                    style={{
                        height: `${props.size}`,
                        width: `${props.size}`,
                        background: `url('${props.image}')`,
                        'background-position': 'center',
                        'background-repeat': 'no-repeat',
                        'background-size': 'cover',
                    }}
                    onClick={() => setPerfil(Perfil)}
                />
            )}
            <Perfil />
            {perfil}
        </div>
    );
}
