import React, { Component } from 'react';
import '../pages/Home.css';
import Comment from '../components/Comment';

export default class Feed extends Component {
    render() {
        const { me, token, head, feed, handleHead } = this.props;
        return (
            <div id="comments">
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
                    />
                ))}
            </div>
        );
    }
}
