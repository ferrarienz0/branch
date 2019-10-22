import React, { Component } from 'react';

export default class Wait extends Component {
    state = {};
    handleSubmit = e => {
        e.preventDefault();
    };
    render() {
        return (
            <div className="start-container">
                <h1>Hello</h1>
            </div>
        );
    }
}
