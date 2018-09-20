import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface ProductData {
    data: string[];
    loading: boolean;
}

export class Cart extends React.Component<RouteComponentProps<{}>, ProductData>
{
    constructor() {
        super();
        this.state = { data: [], loading: true };

        fetch('DataRetrieval/GetAdminPanelInfo')
            .then(response => response.json() as Promise<string[]>)
            .then(data => {
                this.setState({ data: data, loading: false });
            });
    }
}