import styled from 'styled-components';

export const Container = styled.div`
    width: 100%;
    padding: 10px;

    display: flex;
    align-items: center;

    border-bottom: 1px solid var(--dark-magenta);

    &:hover {
        background: var(--gray-1);
        border-bottom: 1px solid var(--magenta);
        transition: 0.1s;
    }

    #user-image {
        position: relative;

        #follow-icon {
            height: 25px;
            width: 25px;
            right: -5px;
            bottom: 0;

            position: absolute;
            cursor: pointer;

            border-radius: 50%;
            color: var(--white);
            background: var(--black);

            &:hover {
                color: var(--light-white);
                transition: 0.1s;
            }
        }
    }

    #text {
        margin-left: 10px;

        #username {
            color: var(--magenta);
            font-size: 20px;
        }

        #name {
            color: var(--gray-4);
            margin-top: 5px;
            font-size: 14px;
        }
    }
`;
