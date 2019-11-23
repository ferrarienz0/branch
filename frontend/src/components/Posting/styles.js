import styled from 'styled-components';

export const Container = styled.div`
    width: 500px;
    height: 300px;
    left: 150px;
    top: 50px;

    z-index: 4;
    position: fixed;
    display: flex;

    #text-area {
        width: 500px;
        height: 300px;
        padding: 5px;
        padding-bottom: 50px;

        resize: none;

        color: var(--white);
        background: rgba(0, 0, 0, 0.8);
        border: none;
        border-bottom: 1px solid var(--dark-blue);
        box-shadow: 8px 8px 8px rgba(0, 0, 0, 0.5);

        &:hover {
            background: rgba(0, 0, 0, 0.9);
            border-bottom: 1px solid var(--blue);
            transition: 0.1s;
        }

        &::-webkit-scrollbar {
            width: 10px;
            cursor: pointer;
        }

        &::-webkit-scrollbar-thumb {
            background: var(--gray-1);

            &:hover {
                background: var(--gray-2);
                transition: 0.1s;
            }
        }
    }

    #send {
        width: 40px;
        height: 40px;
        left: 580px;
        top: 310px;

        cursor: pointer;
        position: fixed;
        display: flex;
        justify-content: center;
        align-items: center;

        background: none;
        border: none;
        &:hover {
            color: var(--light-white);
            transition: 0.1s;
        }
    }

    #media {
        width: 40px;
        height: 40px;
        left: 540px;
        top: 310px;

        cursor: pointer;
        position: fixed;
        display: flex;
        justify-content: center;
        align-items: center;

        border: none;

        &:hover {
            color: var(--light-white);
            transition: 0.1s;
        }
    }

    #media-icon,
    #send-icon {
        height: 20px;
        width: 20px;

        color: var(--white);
    }

    input[type='file'] {
        display: none;
    }

    #preview {
        max-width: 200px;
        max-height: 200px;
        margin: 10px;
        top: 0px;
        left: 500px;
        margin-right: 5px;

        position: absolute;

        border-radius: 5px;
        box-shadow: 8px 8px 8px rgba(0, 0, 0, 0.5);
    }
`;
