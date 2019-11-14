import styled from 'styled-components';
import { Link } from 'react-router-dom';

export const Container = styled.div`
    height: 100%;

    display: flex;
    align-items: center;
    justify-content: center;

    form {
        width: 100%;
        max-width: 300px;

        display: flex;
        align-items: center;
        justify-content: space-between;
        flex-direction: column;
    }

    input {
        height: 48px;
        width: 100%;
        margin: 10px 0 10px 0;

        border: none;
        background: var(--white);
        border-radius: 10px;
        padding: 0 20px;
        font-size: 16px;
        text-align: center;
        box-shadow: var(--shadow);
    }

    p {
        color: var(--orange);
    }
`;

const Button = styled.button`
    height: 48px;
    margin: 10px 0;

    display: flex;
    align-items: center;
    justify-content: center;
    text-align: center;
    cursor: pointer;

    border: 0;
    border-radius: 30px;
    font-size: 16px;
    font-weight: bold;
    color: var(--white);
    box-shadow: var(--shadow);
    text-decoration: none;
`;

export const LogIn = styled(Button)`
    width: 100%;

    background: var(--red);

    p {
        color: var(--white);
    }

    &:hover {
        background: var(--dark-red);
        transition: 0.1s;
    }
`;

export const Register = styled(Link)`
    width: 100%;
    height: 48px;
    margin: 10px 0;

    display: flex;
    align-items: center;
    justify-content: center;
    text-align: center;
    cursor: pointer;

    background: var(--orange);
    border: 0;
    border-radius: 30px;
    font-size: 16px;
    font-weight: bold;
    color: var(--white);
    box-shadow: var(--shadow);
    text-decoration: none;

    &:hover {
        background: var(--dark-orange);
        transition: 0.1s;
    }
`;

export const HalfPugg = styled(Button)`
    height: 80px;
    width: 250px;
    padding: 30px;

    text-align: center;
    cursor: pointer;

    border: 0;
    border-radius: 40px;
    font-size: 16px;
    font-weight: bold;
    background: #22d69a;
    color: #21302a;
    box-shadow: var(--shadow);

    &:hover {
        background: #0f9e6e;
        transition: 0.1s;
    }
`;

export const NotYet = styled.h1`
    text-align: center;

    font-size: 16px;
    color: var(--gray-4);
    margin-top: 40px;
`;
export const Or = styled.h1`
    margin: 50px;
    font-size: 24px;
    color: var(--gray-4);
`;
