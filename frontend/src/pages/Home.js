import React, { Component } from 'react';
import { IoMdPerson, IoMdMenu } from 'react-icons/io';
import {
    FaPaperPlane,
    FaPaperclip,
    FaShoppingCart,
    FaPowerOff,
} from 'react-icons/fa';
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
                    <FaPowerOff id="sair" />
                </div>
                <div id="body">
                    <div id="perfil">
                        <div id="user-image">
                            <UserImage />
                        </div>
                        <FaShoppingCart className="menuIcon" />
                    </div>
                    <div id="topics">
                        <Topic
                            hashtag="LeagueOfLegends"
                            wallpaper="https://lolstatic-a.akamaihd.net/frontpage/apps/prod/LolGameInfo-Harbinger/pt_BR/b49c208c106c3566ac66bc70dc62993c84c0511e/assets/assets/images/gi-landing-top.jpg"
                        />
                        <Topic
                            hashtag="Overwatch"
                            wallpaper="https://observatoriodegames.bol.uol.com.br/wp-content/uploads/2019/09/overwatch.jpg"
                        />
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
