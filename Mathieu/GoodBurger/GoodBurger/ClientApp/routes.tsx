import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Cart } from './components/Cart';
import { Profile } from './components/Profile';
import { Items } from './components/Items';

export const routes = <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/Cart' component={ Cart } />
    <Route path='/Profile' component={Profile} />
    <Route path='/Items' component={Items}/>
</Layout>;
