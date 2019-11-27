import styled from 'styled-components';

export const Container = styled.div`
    display: flex;
    justify-content: left;
    align-items: center;
    flex-direction: column;
`;

export const Header = styled.div`
    width: 100%;
    height: 50px;
    padding: 10px 60px;

    z-index: 3;
    position: fixed;
    display: flex;
    flex-direction: row;

    background: var(--black);
    border-bottom: 1px solid var(--dark-orange);

    &:hover {
        background: var(--gray-1);
        border-bottom: 1px solid var(--orange);
        transition: 0.1s;
    }

    #logo {
        width: 30px;

        flex-direction: row;
        cursor: pointer;
    }

    #go-home {
        width: 30px;
        height: 30px;
        left: 60px;

        position: absolute;
    }

    #space {
        width: 100%;
    }

    #logoff {
        height: 100%;
        width: 25px;
        cursor: pointer;

        color: var(--orange);
    }

    #logoff:hover {
        color: var(--white);
        transition: 0.1s;
    }
`;

export const Body = styled.div`
    width: 100%;

    display: flex;
    flex-direction: row;
    justify-content: center;

    background: var(--black);
`;

export const Perfil = styled.div`
    width: 150px;
    height: 350px;
    padding: 25px 25px 0 25px;
    top: 50px;
    left: 0px;

    position: fixed;
    z-index: 2;
    display: flex;
    align-items: center;
    flex-direction: column;

    background: var(--black);
    border-right: 1px solid var(--dark-orange);
    color: var(--gray-4);
    font-size: 11px;

    &:hover {
        background: var(--gray-1);
        border-color: var(--orange);
        transition: 0.1s;
    }
    #pro-icon {
        width: 30px;
        height: 30px;
        left: 25px;

        position: absolute;

        background: var(--black);
        border-radius: 50%;
        border: 3px solid var(--black);
        color: var(--yellow);
    }

    input {
        visibility: hidden;
    }

    #user-name {
        margin-top: 10px;

        font-size: 20px;
        color: var(--magenta);
    }

    #become-pro {
        margin-top: 20px;

        cursor: pointer;

        font-size: 16px;
        color: var(--yellow);

        &:hover {
            color: var(--white);
            transition: 0.1s;
        }
    }

    #comment-icon,
    #cart-icon,
    #product-icon {
        width: 25px;
        height: 25px;
        margin-top: 20px;

        cursor: pointer;

        color: var(--white);

        &:hover {
            color: var(--light-white);
            transition: 0.1s;
        }
    }
`;

export const Explore = styled.div`
    width: 100%;
    max-width: 1500px;
    margin: 60px 180px 0px 180px;

    display: flex;
`;
