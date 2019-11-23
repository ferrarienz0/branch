import styled from 'styled-components';

export const Container = styled.div`
    flex: 3;

    #users,
    #cart,
    #topics {
        height: 30px;
        margin: 10px 0 5px 0;
        padding: 0 10px;

        cursor: pointer;
        display: flex;
        align-items: center;

        font-style: oblique;

        &:hover {
            color: var(--white);
            background: var(--gray-1);
            transition: 0.1s;
        }
    }

    #users {
        color: var(--dark-magenta);
        border-bottom: 1px solid var(--dark-magenta);
    }

    #cart {
        color: var(--dark-yellow);
        border-bottom: 1px solid var(--dark-yellow);
    }

    #topics {
        color: var(--dark-green);
        border-bottom: 1px solid var(--dark-green);
    }
`;

export const User = styled.div`
    margin: 5px;

    display: flex;
    align-items: center;
    cursor: pointer;

    &:hover {
        transform: translateX(5px);
        transition: 0.1s;
    }

    #name {
        margin-left: 5px;

        color: var(--magenta);
    }
`;
