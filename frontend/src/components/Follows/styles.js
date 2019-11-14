import styled from 'styled-components';

export const Container = styled.div`
    flex: 3;

    #users {
        height: 30px;
        margin: 10px 0 5px 0;
        padding: 0 10px;

        display: flex;
        align-items: center;

        color: var(--dark-magenta);
        font-style: oblique;
        border-bottom: 1px solid var(--dark-magenta);
    }

    #topics {
        height: 30px;
        margin: 10px 0 5px 0;
        padding: 0 10px;

        display: flex;
        align-items: center;

        color: var(--dark-green);
        font-style: oblique;
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
