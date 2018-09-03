import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Cart } from './components/Cart';
import { Profile } from './components/Profile';
import { Items } from './components/Items';
import { FetchData } from './components/Templates/FetchData'
import { ItemDescription } from './components/ItemDescription'

export const routes = <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/Cart' component={Cart} />
    <Route path='/Profile' component={FetchData} />
    <Route path='/Items' component={Items} />
    <Route path='/Item/' component={ItemDescription}/>
</Layout>;
