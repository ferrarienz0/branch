import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import {
    FaPaperPlane,
    FaPaperclip,
    FaShoppingCart,
    FaPowerOff,
    FaComment,
} from 'react-icons/fa';
import './Home.css';
import icone from '../assets/icone.svg';
import api from '../services/api';
import Topic from '../components/Topic';
import Post from '../components/Post';
import Posting from '../components/Posting';
import UserImage from '../components/UserImage';

export default class Home extends Component {
    state = {
        posting: false,
        user: {
            name: '',
            lastname: '',
            username: '',
            email: '',
            image: '',
            topics: [{ hashtag: '', wallpaper: '' }],
        },
        users: [
            {
                name: 'Marcos',
                lastname: 'Loli',
                username: 'xxMarc0sxx',
                image:
                    'https://i.pinimg.com/originals/04/b7/4b/04b74ba86504a599a8aeb5b57251ed69.png',
            },
            {
                name: 'Lucas',
                lastname: 'Rezende',
                username: 'oLucasRez',
                image:
                    'https://scontent-lga3-1.cdninstagram.com/v/t51.2885-15/e35/23416406_188817468343614_6756926877455089664_n.jpg?_nc_ht=scontent-lga3-1.cdninstagram.com&se=8&oh=be02f6738a562126d89d6af0b3737a40&oe=5DEF0867',
            },
            {
                name: 'Enzo',
                lastname: 'Ferrari',
                username: 'FerrariEnz0',
                image: 'https://i.ytimg.com/vi/CW7t2HwZLuw/maxresdefault.jpg',
            },
            {
                name: 'João',
                lastname: 'Silva',
                username: 'Sam321h',
                image:
                    'https://vignette.wikia.nocookie.net/naruto/images/8/86/Casual_Haku.png/revision/latest?cb=20150123172229',
            },
        ],
        posts: [
            {
                user: 0,
                text: '.',
                image: null,
                likes: [],
                dislikes: [],
                parent: null,
                children: [1, 2],
            },
            {
                user: 1,
                text: 'Ola, meu povo',
                image: null,
                likes: [],
                dislikes: [],
                parent: 1,
                children: [],
            },
            {
                user: 2,
                text: 'oe',
                image: null,
                likes: [],
                dislikes: [],
                parent: 1,
                children: [3],
            },
            {
                user: 3,
                text: '',
                image:
                    'data:image/jpeg;base64,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',
                likes: [],
                dislikes: [],
                parent: 2,
                children: [],
            },
        ],
        topics: [
            {},
            {},
            {},
            {},
            //{
            //  hashtag: 'LeagueOfLegends',
            // wallpaper:
            //    'https://www.leak.pt/wp-content/uploads/2019/09/league-of-legends-1-e1568812715634.jpg',
            //},
            //{
            //    hashtag: 'Overwatch',
            //    wallpaper:
            //        'https://observatoriodegames.bol.uol.com.br/wp-content/uploads/2019/09/overwatch.jpg',
            //},
            // {
            //    hashtag: 'Dota2',
            //    wallpaper:
            //        'https://external-preview.redd.it/L-269XDCtpNmS9KaO6oq-N3BcmV1Okn4gmfTbWD_qwk.jpg?auto=webp&s=47ac4a7b1de3cff1bed359873ab5df087250e1d2',
            // },
            // {
            //     hashtag: 'CounterStrikeGO',
            //     wallpaper: 'https://wallpaperaccess.com/full/147194.jpg',
            // },
        ],
    };
    componentDidMount = async () => {
        const { data: data1 } = await api.get(
            `/user?AccessToken=${this.props.match.params.token}`
        );
        const { data: topics } = await api.get(
            `/userInterests?AccessToken=${this.props.match.params.token}`
        );
        this.setState({
            user: {
                name: decodeURIComponent(data1.Firstname),
                lastname: decodeURIComponent(data1.Lastname),
                username: decodeURIComponent(data1.Nickname),
                email: data1.Email,
                topics,
            },
        });
        if (this.state.user.topics.length === 0) {
            let aux = [];
            await api.get(`/subject?id=${1}`).then(res => {
                console.log(res.data);
                //aux.push(res);
            });
            await api.get(`/subject?id=${2}`).then(res => {
                aux.push(res.data);
            });
            await api.get(`/subject?id=${3}`).then(res => {
                aux.push(res.data);
            });
            await api.get(`/subject?id=${4}`).then(res => {
                aux.push(res.data);
            });

            for (var i = 0; i < aux.length; i++) {
                aux[i].wpp = (
                    <img
                        src={
                            (await api.get(`/media?Id=${aux[i].MediaId}`)).data
                        }
                    />
                );
            }

            console.log(aux);

            this.setState({ topics: aux });
        }
    };
    showPosting = () => {
        return (
            <div id="posting-container">
                <textarea type="text" />
                <div className="optionsPost">
                    <button className="send">
                        <FaPaperPlane className="sendIcon" />
                    </button>
                    <FaPaperclip className="clipIcon" />
                </div>
            </div>
        );
    };
    handleUserPost = () => {};
    render() {
        return (
            <div id="home-container">
                <div id="head">
                    <div id="logo">
                        <img src={icone} alt="Branch"></img>
                        <Link
                            id="go-home"
                            to={`/home/${this.props.match.params.token}`}
                        />
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
                            image={this.state.user.image}
                        />
                        <strong id="user-name">
                            @{this.state.user.username}
                        </strong>
                        <p id="name">
                            {this.state.user.name} {this.state.user.lastname}
                        </p>
                        <FaComment
                            id="comment-icon"
                            onClick={e =>
                                this.state.posting
                                    ? this.setState({ posting: false })
                                    : this.setState({ posting: true })
                            }
                        />
                        <FaShoppingCart id="cart-icon" />
                    </div>
                    <div id="posts">
                        <Post
                            head=""
                            post={this.state.posts[1]}
                            user={this.state.users[1]}
                        />
                        <Post
                            post={this.state.posts[2]}
                            type="#"
                            user={this.state.users[2]}
                        />
                        <Post
                            type="@"
                            post={this.state.posts[0]}
                            user={this.state.users[0]}
                        />
                        <Post
                            post={this.state.posts[3]}
                            type="$"
                            user={this.state.users[3]}
                        />
                        <Post
                            post={this.state.posts[1]}
                            type=">"
                            user={this.state.users[3]}
                        />
                    </div>
                    <div id="topics">
                        {this.state.topics.map((topic, index) => (
                            <Topic
                                key={index}
                                hashtag={topic.Hashtag}
                                wallpaper={topic.wpp}
                            />
                        ))}
                    </div>
                </div>
                {this.state.posting ? (
                    <Posting
                        user={this.state.user.username}
                        token={this.props.match.params.token}
                        onClose={() => this.setState({ posting: false })}
                    />
                ) : null}
            </div>
        );
    }
}
