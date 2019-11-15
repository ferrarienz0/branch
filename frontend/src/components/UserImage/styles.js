import styled from 'styled-components';

export const Container = styled.div`
    width: ${props => props.size};
    height: ${props => props.size};

    cursor: pointer;

    #user-icon {
        height: ${props => props.size};
        width: ${props => props.size};

        color: var(--gray-4);
        background: url(${props => props.image});
        background-position: center;
        background-repeat: no-repeat;
        background-size: cover;
    }

    #user-image {
        height: ${props => props.size};
        width: ${props => props.size};

        border-radius: 50%;
        color: var(--gray-4);
        background: url(${props => props.image});
        background-position: center;
        background-repeat: no-repeat;
        background-size: cover;
    }
`;
