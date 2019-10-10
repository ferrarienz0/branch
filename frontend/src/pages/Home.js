import React, { Component } from 'react';
import { IoMdPerson, IoMdMenu } from 'react-icons/io';
import { FaPaperPlane, FaPaperclip, FaShoppingCart } from 'react-icons/fa';
import './Home.css';
import icone from '../assets/icone.svg';
import HeadPost from '../components/HeadPost';
import Topic from '../components/Topic';
import Post from '../components/Post';
import UserImage from '../components/UserImage';

export default class Home extends Component {
    state = {
        image: null,
    };
    render() {
        return (
            <div id="home-container">
                <div id="head">
                    <div id="logo">
                        <img src={icone} alt="Branch" />
                    </div>
                    <div id="space" />
                    <p id="sair">
                        <strong>Sair</strong>
                    </p>
                </div>
                <div id="body">
                    <div id="perfil">
                        <div id="user-image">
                            <UserImage />
                        </div>
                        <FaShoppingCart className="menuIcon" />
                    </div>
                    <div id="topics">
                        <Topic topicID="" />
                    </div>
                    <div id="posts">
                        <HeadPost postID="" />
                        <Post postID="" />
                        <Post postID="" />
                    </div>
                </div>
            </div>
        );
    }
}
