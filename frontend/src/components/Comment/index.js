import React, { Component } from 'react';
import {
    FaThumbsUp,
    FaThumbsDown,
    FaRegThumbsUp,
    FaRegThumbsDown,
    FaComment,
    FaArrowUp,
    FaCartPlus,
    FaPlus,
    FaTimes,
} from 'react-icons/fa';
import { Container, Header, Body, Footer } from './styles';
import UserImage from '../UserImage';
import api from '../../services/api';

export default class Comment extends Component {
    state = {
        iLiked: false,
        nLikes: this.props.comment.Likes.length,
        iDisliked: false,
        nDislikes: this.props.comment.Dislikes.length,
        iFollow: false,
        text: [],
    };

    componentDidMount = async () => {
        const { token, me, comment } = this.props;
        const { data: iFollow } = await api.get(
            `/follows?AccessToken=${token}`
        );
        iFollow.forEach(followed => {
            if (followed.Id === comment.Owner.Id) {
                this.setState({ iFollow: true });
            }
        });
        if (comment.Likes.indexOf(me.ID) !== -1) {
            this.setState({
                iLiked: true,
            });
        }
        if (comment.Dislikes.indexOf(me.ID) !== -1) {
            this.setState({
                iDisliked: true,
            });
        }
    };

    handleLike = async () => {
        const { iLiked } = this.state;
        const { token, comment } = this.props;
        const { data } = await api.put(
            `/post/like?AccessToken=${token}&PostId=${comment.Id}`
        );
        this.setState({
            nLikes: data.TotalLikes,
            nDislikes: data.TotalDeslikes,
        });
        if (iLiked) this.setState({ iLiked: false });
        else this.setState({ iLiked: true, iDisliked: false });
    };

    handleDislike = async () => {
        const { token, comment } = this.props;
        const { data } = await api.put(
            `/post/dislike?AccessToken=${token}&PostId=${comment.Id}`
        );
        this.setState({
            nLikes: data.TotalLikes,
            nDislikes: data.TotalDeslikes,
        });
        if (this.state.iDisliked) this.setState({ iDisliked: false });
        else this.setState({ iDisliked: true, iLiked: false });
    };

    handleFollow = () => {
        const { iFollow } = this.state;
        const { comment, token } = this.props;
        if (!iFollow) {
            api.post(
                `/follow/create?AccessToken=${token}&RequestedUserId=${comment.Owner.Id}`
            ).then(res => {
                this.setState({ iFollow: true });
            });
        } else {
            api.delete(
                `/follow/delete?AccessToken=${token}&FollowedId=${comment.Owner.Id}`
            ).then(res => {
                this.setState({ iFollow: false });
            });
        }
    };

    handleText = () => {
        const { comment, handleHead } = this.props;
        let res = [],
            u = 0,
            t = 0;
        let text = comment.Text.split(' ');
        text.forEach(word => {
            if (word[0] === '@')
                res.push(
                    <i
                        id="user"
                        onClick={() =>
                            handleHead('user', comment.Mentions[u++])
                        }
                    >
                        {word}{' '}
                    </i>
                );
            else if (word[0] === '#')
                res.push(
                    <i
                        id="topic"
                        onClick={() =>
                            handleHead('topic', comment.Hashtags[t++])
                        }
                    >
                        {word}{' '}
                    </i>
                );
            else res.push(word + ' ');
        });
        return res;
    };

    render() {
        const { comment, onHead, handleHead, head, me } = this.props;
        const { iLiked, nLikes, iDisliked, nDislikes, iFollow } = this.state;
        return (
            <Container head={head}>
                <Header>
                    <div id="user-image">
                        <UserImage size="50px" image={comment.Owner.MediaURL} />
                        {me.ID === comment.Owner.Id ? null : iFollow ? (
                            <FaTimes
                                id="follow-icon"
                                onClick={this.handleFollow}
                            />
                        ) : (
                            <FaPlus
                                id="follow-icon"
                                onClick={this.handleFollow}
                            />
                        )}
                    </div>
                    <div id="user-name">
                        <strong
                            onClick={() => handleHead('user', comment.Owner.Id)}
                        >
                            @{comment.Owner.Nickname}
                        </strong>
                        <p id="name">
                            {comment.Owner.Firstname} {comment.Owner.Lastname}
                        </p>
                    </div>
                    {head ? null : (
                        <div id="go-ahead">
                            <FaArrowUp id="go-ahead-icon" onClick={onHead} />
                        </div>
                    )}
                </Header>
                <Body>
                    <div id="text">
                        {this.handleText().map((word, index) => (
                            <p id="word" key={index}>
                                {word}
                            </p>
                        ))}
                    </div>
                    <img
                        id="image"
                        alt=""
                        src={
                            comment.MediaObjects.length === 0
                                ? ''
                                : comment.MediaObjects[0].URL
                        }
                    />
                </Body>
                <Footer>
                    <p id="number">
                        <strong>{nDislikes}</strong>
                    </p>
                    {iDisliked ? (
                        <FaThumbsDown
                            onClick={this.handleDislike}
                            id="dislike-icon"
                        />
                    ) : (
                        <FaRegThumbsDown
                            onClick={this.handleDislike}
                            id="dislike-icon"
                        />
                    )}
                    <p id="number">
                        <strong>{nLikes}</strong>
                    </p>
                    {iLiked ? (
                        <FaThumbsUp onClick={this.handleLike} id="like-icon" />
                    ) : (
                        <FaRegThumbsUp
                            onClick={this.handleLike}
                            id="like-icon"
                        />
                    )}
                    <FaComment id="comment-icon" />
                    {this.props.me.is_product == null ? null : (
                        <FaCartPlus id="cart-icon" />
                    )}
                </Footer>
            </Container>
        );
    }
}
