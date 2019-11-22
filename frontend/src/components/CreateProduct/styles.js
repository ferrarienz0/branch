import styled from 'styled-components';

export const Container = styled.div`
    width: 500px;
    height: 300px;
    left: 150px;
    top: 50px;
    padding: 20px;

    z-index: 4;
    position: fixed;
    display: flex;
    flex-direction: row;

    background: var(--black);
    border-right: 1px solid var(--dark-yellow);
    border-bottom: 1px solid var(--dark-yellow);

    &:hover {
        border-right: 1px solid var(--yellow);
        border-bottom: 1px solid var(--yellow);
        background: var(--gray-1);
        transition: 0.1s;
    }

    #aux {
        width: 100px;
    }

    input {
        width: 219px;
        height: 25px;
        margin: 5px;
        padding-left: 5px;

        border-radius: 3px;
        border: none;
        background: var(--white);
    }

    #description {
        width: 219px;
        height: 75px;
        margin: 5px;
        padding: 5px;

        resize: none;

        border-radius: 3px;
        border: none;
        background: var(--white);
    }

    #media {
        width: 100px;
        height: 100px;
        left: 475px;
        top: 150px;

        cursor: pointer;
        position: fixed;
        display: flex;
        justify-content: center;
        align-items: center;

        color: var(--white);
        border: none;

        &:hover {
            color: var(--light-white);
            transition: 0.1s;
        }
    }

    #media-icon {
        width: 100%;
        height: 100%;

        color: var(--gray-3);
    }

    #send-icon {
        width: 25px;
        height: 25px;
        top: 260px;
        left: 460px;

        cursor: pointer;
        position: absolute;

        color: var(--yellow);

        &:hover {
            color: var(--white);
            transition: 0.1s;
        }
    }

    input[type='file'] {
        display: none;
    }

    #preview-div {
        width: 210px;
        height: 250px;
        margin: 10px;
        top: 15px;
        left: 250px;
        margin-right: 5px;

        display: flex;
        align-items: center;
        justify-content: center;
        position: absolute;
    }

    #preview {
        width: 100%;
        max-width: 210px;
        max-height: 250px;

        border-radius: 3px;
    }
`;
