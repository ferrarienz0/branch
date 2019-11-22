import React, { Component } from 'react';
import { Container } from './styles';

export default class ProductHead extends Component {
    render() {
        const { me } = this.props;
        return (
            <Container>
                {/*<Header>
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
                    <FaComment
                        id="comment-icon"
                        onClick={() => onPosting(comment.Id)}
                    />
                    {comment.Parent === null ? null : (
                        <FaReply
                            id="answered-icon"
                            onClick={() =>
                                handleHead('comment', comment.Parent)
                            }
                        />
                    )}
                </Footer>*/}
            </Container>
        );
    }
}
