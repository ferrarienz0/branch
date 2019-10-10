import React, { Component } from 'react';
import { IoMdPerson, IoMdMenu } from 'react-icons/io';
import { FaPaperPlane, FaPaperclip, FaShoppingCart } from 'react-icons/fa';
import './Posting.css';

export default class Posting extends Component {
    state = {
        image: null,
    };
    render() {
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
    }
}
