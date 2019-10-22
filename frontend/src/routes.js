import React from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import Login from './pages/Login';
import Home from './pages/Home';
import Register from './pages/Register';
import Wait from './pages/Wait';
export default function Routes() {
    return (
        <BrowserRouter>
            <Switch>
                <Route path="/" exact component={Login} />
                <Route path="/home/:token" component={Home} />
                <Route path="/register" component={Register} />
                <Route path="/wait" component={Wait} />
            </Switch>
        </BrowserRouter>
    );
}
