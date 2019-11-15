import React, { Component } from 'react';
import { Container } from './styles';
import Comment from '../../components/Comment';

export default class Feed extends Component {
    render() {
        const { me, token, head, feed, handleHead, onPosting } = this.props;
        return (
            <Container>
                {head}
                {feed.comments.map((comment, index) => (
                    <Comment
                        key={index}
                        head={false}
                        me={me}
                        comment={comment}
                        token={token}
                        onHead={() => handleHead('comment', comment.Id)}
                        handleHead={handleHead}
                        onPosting={onPosting}
                    />
                ))}
            </Container>
        );
    }
}
