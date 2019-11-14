import styled from 'styled-components';

export const Container = styled.div`
    height: 70px;
    margin: 0 0 5px 5px;
    padding: 5px 10px;

    display: flex;
    align-items: center;
    cursor: pointer;

    border-bottom: 1px solid var(--dark-green);
    background-image: linear-gradient(
            135deg,
            var(--black) 25%,
            transparent 110%
        ),
        url(${props => props.banner});
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;

    &:hover {
        border-bottom: 1px solid var(--green);
        transform: translateX(5px);
        transition: 0.1s;
    }

    h2 {
        color: var(--green);
        text-shadow: var(--shadow);
    }
`;

export const Footer = styled.div`
    height: 30px;
    width: 100%;
    padding-right: 10px;

    display: flex;
    flex-direction: row-reverse;
    align-items: center;

    #comment-icon,
    #follow-icon,
    #go-ahead-icon {
        height: 100%;
        width: 25px;
        margin-left: 10px;

        cursor: pointer;

        color: var(--white);

        &:hover {
            color: var(--light-white);
            transition: 0.1s;
        }
    }
`;
