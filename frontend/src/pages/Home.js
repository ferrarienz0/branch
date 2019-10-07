import React, { Component } from 'react';
import { IoMdPerson, IoMdMenu } from 'react-icons/io';
import { FaRegPaperPlane, FaImage } from 'react-icons/fa';
import { MdGif } from 'react-icons/md';
import './Home.css';
import icone from '../assets/icone.svg';

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
                        <div className="photo">
                            {this.image == null ? (
                                <IoMdPerson className="userIcon" />
                            ) : (
                                <img src={this.image} />
                            )}
                        </div>
                        <IoMdMenu className="menuIcon" />
                    </div>
                    <div className="feed">
                        <div className="myPost">
                            <input type="text" />
                            <div className="optionsPost">
                                <button className="send">
                                    <FaRegPaperPlane className="sendIcon" />
                                </button>
                                <FaImage className="imageIcon" />
                                <MdGif className="gifIcon" />
                            </div>
                        </div>
                    </div>
                    <div className="tips"></div>
                </div>
            </div>
        );
    }
}
