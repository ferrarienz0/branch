import React, { Component } from 'react';
import '../pages/Home.css';
import api from '../services/api';
import Comment from '../components/Comment';
import UserHead from '../components/Heads/UserHead';
import TopicHead from '../components/Heads/TopicHead';
import CommentHead from '../components/Heads/CommentHead';
import ProductHead from '../components/Heads/ProductHead';

export default class Feed extends Component {
    state = {
        head: <div />,
        comments: [],
    };

    componentDidMount = () => {
        this.getHead();
        this.getComments();
    };

    getHead = async () => {
        const { token, me, type, id } = this.props;
        switch (type) {
            case 'u':
                this.setState({
                    head: (
                        <UserHead me={me} userID={id} refresh={this.refresh} />
                    ),
                });
                break;
            case 'h':
                this.setState({
                    head: (
                        <TopicHead
                            me={me}
                            token={token}
                            topicID={id}
                            refresh={this.refresh}
                        />
                    ),
                });
                break;
            case 'c':
                const { data: comment } = await api.get(`/posts?PostId=${id}`);
                this.setState({
                    head: (
                        <CommentHead
                            me={me}
                            token={token}
                            comment={comment}
                            refresh={this.refresh}
                        />
                    ),
                });
                break;
            case '$':
                this.setState({
                    head: (
                        <ProductHead
                            me={me}
                            token={token}
                            productID={id}
                            refresh={this.refresh}
                        />
                    ),
                });
                break;
            default:
                this.setState({ head: <div /> });
        }
    };

    getComments = async () => {
        const { id, type } = this.props;
        let req = '';
        switch (type) {
            case 'h':
                req = `/subject/posts?SubjectId=${id}`;
                break;
            case 'c':
                req = `/post/comments?PostId=${id}`;
                break;
            case 'u':
                req = `/user/posts?UserId=${id}`;
                break;
            default:
                req = '';
        }
        if (req !== '') {
            const { data: comments } = await api.get(req);
            this.setState({ comments });
        }
    };

    render() {
        const { head, comments } = this.state;
        const { me, token, handleHead } = this.props;
        return (
            <div id="comments">
                {head}
                {comments.map((comment, index) => (
                    <Comment
                        key={index}
                        me={me}
                        comment={comment}
                        token={token}
                        handleHead={handleHead}
                    />
                ))}
            </div>
        );
    }
}
