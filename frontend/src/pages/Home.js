import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { FaShoppingCart, FaPowerOff } from 'react-icons/fa';
import './Home.css';
import icone from '../assets/icone.svg';
import Topic from '../components/Topic';
import Post from '../components/Post';
import UserImage from '../components/UserImage';

export default class Home extends Component {
    state = {
        image: null,
        posts: [
            {
                user: {
                    name: 'Marcos',
                    lastname: 'Loli',
                    username: 'xxMarc0sxx',
                    image:
                        'https://i.pinimg.com/originals/04/b7/4b/04b74ba86504a599a8aeb5b57251ed69.png',
                },
            },
        ],
        users: [
            {
                id: 0,
                username: 'xxMarc0sxx',
                image:
                    'https://scontent-lga3-1.cdninstagram.com/v/t51.2885-15/e35/23416406_188817468343614_6756926877455089664_n.jpg?_nc_ht=scontent-lga3-1.cdninstagram.com&se=8&oh=be02f6738a562126d89d6af0b3737a40&oe=5DEF0867',
            },
            {
                id: 1,
                username: 'oLucasRez',
                image:
                    'https://i.pinimg.com/originals/04/b7/4b/04b74ba86504a599a8aeb5b57251ed69.png',
            },
            {
                id: 2,
                username: 'FerrariEnz0',
                image: 'https://i.ytimg.com/vi/CW7t2HwZLuw/maxresdefault.jpg',
            },
            {
                id: 3,
                username: 'Sam321h',
                image:
                    'https://vignette.wikia.nocookie.net/naruto/images/8/86/Casual_Haku.png/revision/latest?cb=20150123172229',
            },
        ],
    };
    render() {
        return (
            <div id="home-container">
                <div id="head">
                    <div id="logo">
                        <img src={icone} alt="Branch" />
                    </div>
                    <div id="space" />
                    <Link to="/">
                        <FaPowerOff id="sair" />
                    </Link>
                </div>
                <div id="body">
                    <div id="perfil">
                        <UserImage
                            id="user-image"
                            size="100px"
                            image={this.state.users[0].image}
                        />
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
                        <Post head="" postID="" user={this.state.users[1]} />
                        <Post postID="" user={this.state.users[2]} />
                        <Post postID="" user={this.state.users[3]} />
                    </div>
                </div>
            </div>
        );
    }
}
