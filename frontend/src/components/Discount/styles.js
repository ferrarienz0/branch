import styled from 'styled-components';

export const Container = styled.div`
    margin-left: 20px;
    padding: 5px 10px;

    display: flex;
    align-items: center;

    &:hover {
        transform: translateX(5px);
        transition: 0.1s;
    }

    #col {
        display: flex;
        flex-direction: column;
    }

    #row {
        display: flex;
        flex-direction: row;
        align-items: center;
    }

    #amount {
        font-size: 20px;
        margin: 0 4px;

        color: var(--white);
    }

    #name {
        font-size: 20px;
        margin-right: 40px;

        cursor: pointer;

        color: var(--yellow);

        &:hover {
            color: var(--white);
            transition: 0.1s;
        }
    }

    #price {
        font-size: 20px;

        color: var(--white);
    }

    i {
        margin-left: 30px;
        color: var(--yellow);
        cursor: pointer;

        &:hover {
            color: var(--white);
            transition: 0.1s;
        }
    }

    #image {
        height: 50px;
        width: 50px;
        margin-right: 15px;

        border-radius: 50%;
        color: var(--gray-4);
        background: url(${props => props.image});
        background-position: center;
        background-repeat: no-repeat;
        background-size: cover;
    }
`;
