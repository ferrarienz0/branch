import React, { Component } from 'react';
import { IoMdPerson, IoMdMenu } from 'react-icons/io';
import {
    FaUserCircle,
    FaPaperPlane,
    FaImage,
    FaPaperclip,
    FaRegThumbsUp,
    FaRegThumbsDown,
} from 'react-icons/fa';
import { MdGif } from 'react-icons/md';
import './Home.css';
import icone from '../assets/icone.svg';
import Post from './Post';
import UserImage from './UserImage';

export default class Home extends Component {
    state = {
        image: null,
    };
    render() {
        return (
            <div className="home-container">
                <link
                    href="https://fonts.googleapis.com/icon?family=Material+Icons"
                    rel="stylesheet"
                />
                <div className="head">
                    <div>
                        <img src={icone} alt="Branch" />
                    </div>
                </div>
                <div className="body">
                    <div className="perfil">
                        <div>
                            <UserImage/>
                        </div>
                        <IoMdMenu className="menuIcon" />
                    </div>
                    <div className="feed">
                        <div className="myPost">
                            <textarea type="text" />
                            <div className="optionsPost">
                                <button className="send">
                                    <FaPaperPlane className="sendIcon" />
                                </button>
                                <FaPaperclip className="clipIcon" />
                            </div>
                        </div>
                        <Post postID=""/>
                        <Post postID=""/>
                    </div>
                    <div className="tips"></div>
                </div>
            </div>
        );
    }
}
