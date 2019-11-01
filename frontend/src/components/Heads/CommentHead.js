import React, { Component } from 'react';
import './CommentHead.css';
import UserImage from '../UserImage';

export default class CommentHead extends Component {
    componentDidMount = async () => {};
    render() {
        console.log(this.props.head);
        return (
            <div id="commenthead-container">
                <div />
            </div>
        );
    }
}
