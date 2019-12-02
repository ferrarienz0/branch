import styled from 'styled-components';

export const Container = styled.div`
    margin: 0 0 5px 5px;

    border-bottom: 1px solid var(--dark-yellow);

    &:hover {
        background: var(--gray-1);
        border-bottom: 1px solid var(--yellow);
        transition: 0.1s;
    }
`;

export const Header = styled.div`
    height: 60px;
    padding: 5px 20px 5px 5px;

    display: flex;
    flex-direction: row;
    align-items: center;

    background: none;
    border: none;

    i {
        margin-left: 15px;
        color: var(--gray-4);
    }

    #name {
        font-size: 22px;
        color: var(--yellow);
    }

    #pro-name {
        color: var(--magenta);
        cursor: pointer;

        &:hover {
            color: var(--white);
            transition: 0.1s;
        }
    }
`;

export const Body = styled.div`
    width: 100%;
    padding: 10px;

    border: none;
    display: flex;
    flex-direction: row;

    background: none;

    img {
        width: 100%;
        max-width: 250px;
        max-height: 350px;
        padding: 10px;

        flex: 1;
    }

    #right {
        width: 100%;
        padding-left: 15px;

        display: flex;
        flex-direction: column;
        flex: 1;

        #price {
            font-size: 32px;
            color: var(--white);
        }

        #description {
            font-size: 16px;
            color: var(--gray-4);
        }
    }
`;

export const Footer = styled.div`
    height: 40px;
    padding: 0 20px;

    display: flex;
    flex-direction: row-reverse;
    align-items: center;

    #add-cart {
        width: 25px;
        height: 25px !important;
        margin-right: 10px;

        cursor: pointer;

        color: var(--white);

        &:hover {
            color: var(--light-white);
            transition: 0.1s;
        }
    }

    #stock {
        font-size: 16px;
        margin-right: 10px;
        color: var(--gray-4);
    }

    input {
        width: 50px;
        font-size: 18px;
        background: none;
        color: var(--gray-4);
        border: none;
    }
`;
