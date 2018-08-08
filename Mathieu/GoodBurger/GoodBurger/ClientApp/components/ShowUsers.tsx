import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface UserData {
    data: string[];
    loading: boolean;
}

export class ShowUsers extends React.Component<RouteComponentProps<{}>, UserData>
{
    constructor() {
        super();
        this.state = {data: [""], loading: true };

        fetch('api/DataRetrieval/GetNames')
            .then(response => response.json() as Promise<string[]>)
            .then(data => {
                this.setState({ data: data, loading: false });
            });
    }

    public render() {
        return  <ul> 
                    <li>{this.state.data[0]}</li>
                    <li>{this.state.data[1]}</li>
                </ul> 
    }

    
}