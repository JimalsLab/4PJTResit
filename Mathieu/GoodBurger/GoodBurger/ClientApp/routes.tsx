import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { ShowUsers } from './components/ShowUsers';

export const routes = <Layout>
    <Route exact path='/' component={ShowUsers} />
    <Route path='/counter' component={ Counter } />
    <Route path='/fetchdata' component={FetchData} />
    <Route path='/showusers' component={ShowUsers} />
</Layout>;
