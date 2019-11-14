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
        justify-content: space-between;
        align-items: center;
        flex-direction: column;
    }

    input {
        height: 40px;
        width: 100%;
        margin: 5px 0 5px 0;
        padding: 0 20px;

        text-align: center;

        border: none;
        background: var(--white);
        border-radius: 10px;
        font-size: 16px;
        box-shadow: var(--shadow);
        text-align: center;
    }

    p {
        margin-top: 5px;

        color: var(--orange);
    }
`;

export const Submit = styled(Link)`
    width: 100%;
    height: 48px;
    margin-top: 10px;

    display: flex;
    align-items: center;
    justify-content: center;
    text-align: center;
    cursor: pointer;
    text-decoration: none;

    border: 0;
    border-radius: 30px;
    font-size: 16px;
    font-weight: bold;
    color: var(--white);
    background: var(--red);
    box-shadow: var(--shadow);

    &:hover {
        background: var(--dark-red);
        transition: 0.1s;
    }
`;

export const Calendar = styled.input`
    color: var(--gray-4);
    background: var(--white)
        url(https://cdn1.iconfinder.com/data/icons/cc_mono_icon_set/blacks/16x16/calendar_2.png)
        90% 50% no-repeat !important;

    &::-webkit-inner-spin-button {
        display: none;
    }

    &::-webkit-calendar-picker-indicator {
        opacity: 0;
    }
`;
