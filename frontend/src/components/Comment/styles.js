import styled from 'styled-components';

export const Container = styled.div`
    margin: ${props => (props.head ? '0 0 5px 5px' : '5px 5px 5px 40px')};

    border-bottom: 1px solid var(--dark-blue);

    &:hover {
        background: var(--gray-1);
        border-bottom: 1px solid var(--blue);
        transform: ${props => (props.head ? '' : 'translateX(-5px)')};
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

    #user-image {
        position: relative;

        #follow-icon {
            height: 20px;
            width: 20px;
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

    #user-name {
        margin-left: 10px;

        cursor: pointer;

        color: var(--magenta);
    }

    #name {
        color: var(--gray-4);
        font-size: 11px;
    }

    #go-ahead {
        display: flex;
        flex: 1;
        align-items: center;
        flex-direction: row-reverse;
        cursor: pointer;

        #go-ahead-icon {
            height: 25px;
            width: 25px;

            cursor: pointer;

            color: var(--white);

            &:hover {
                color: var(--light-white);
                transition: 0.1s;
            }
        }
    }
`;

export const Body = styled.div`
    width: 100%;
    padding: 10px;

    border: none;
    display: flex;
    flex-direction: column;

    background: none;

    #text {
        color: var(--white);
        display: flex;
        flex-wrap: wrap;

        #word {
            margin: 2px;

            #user {
                color: var(--magenta);
                cursor: pointer;

                &:hover {
                    color: var(--white);
                    transition: 0.1s;
                }
            }

            #topic {
                color: var(--green);
                cursor: pointer;

                &:hover {
                    color: var(--white);
                    transition: 0.1s;
                }
            }
        }
    }

    #image {
        width: 100%;
        max-width: 350px;
        padding: 10px;
    }
`;

export const Footer = styled.div`
    height: 40px;
    padding: 0 10px;

    display: flex;
    flex-direction: row-reverse;
    align-items: center;

    #number {
        color: var(--white);
    }

    #comment-icon,
    #cart-icon,
    #like-icon,
    #dislike-icon {
        height: 25px;
        width: 25px;
        margin: 0 5px;

        cursor: pointer;

        &:hover {
            color: var(--light-white);
            transition: 0.1s;
        }
    }

    #comment-icon {
        height: 25px;
        width: 25px;

        cursor: pointer;

        color: var(--white);
    }

    #like-icon {
        margin-bottom: 10px;
        color: var(--green);
    }

    #dislike-icon {
        color: var(--orange);
    }
`;
