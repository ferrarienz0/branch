import React, { Component } from 'react';
import './TopicHead.css';
import { FaComment, FaPlus, FaTimes } from 'react-icons/fa';
import api from '../../services/api';

export default class TopicHead extends Component {
    state = {
        topic: {},
        wallpaper: '',
    };
    componentWillMount = async () => {
        const { data: topic } = await api.get(
            `/subject?id=${this.props.topicID}`
        );
        this.setState({ topic, wallpaper: topic.Media.URL });
    };
    render() {
        return (
            <div
                id="topichead-container"
                style={{
                    backgroundImage: `linear-gradient(
                    135deg,
                    var(--black) 25%,
                    transparent 110%
                ),  url(${this.state.wallpaper})`,
                    backgroundPosition: 'center',
                    backgroundRepeat: 'no-repeat',
                    backgroundSize: 'cover',
                }}
            >
                <h2 id="hashtag">{this.state.topic.Hashtag}</h2>
                <div id="foot">
                    <FaComment
                        id="comment"
                        onClick={e =>
                            this.state.posting
                                ? this.setState({ posting: false })
                                : this.setState({ posting: true })
                        }
                    />
                    {this.state.followed ? (
                        <FaTimes id="follow-icon" onClick={this.handleFollow} />
                    ) : (
                        <FaPlus id="follow-icon" onClick={this.handleFollow} />
                    )}
                </div>
            </div>
        );
    }
}
