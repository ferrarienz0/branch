import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

export default class Pog extends Component {
    render() {
        const { token, type, id } = this.props.match.params;
        if (type === undefined) return <Redirect to={`/home/${token}`} />;
        else return <Redirect to={`/home/${token}/${type}/${id}`} />;
    }
}
