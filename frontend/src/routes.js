import React from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import Login from './pages/Login';
import Home from './pages/Home';
import Pog from './pages/Pog.js';
import Register from './pages/Register';

export default function Routes() {
    return (
        <BrowserRouter>
            <Switch>
                <Route path="/" exact component={Login} />
                <Route path="/home/:token/:type?/:id?" exact component={Home} />
                <Route path="/pog/:token/:type?/:id?" exact component={Pog} />
                <Route path="/register" exact component={Register} />
            </Switch>
        </BrowserRouter>
    );
}
