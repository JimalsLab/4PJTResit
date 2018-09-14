import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface UserData {
    data: string[];
    loading: boolean;
}

export class Profile extends React.Component<RouteComponentProps<{}>, UserData>
{
    constructor() {
        super();
        this.state = { data: [""], loading: true };

        fetch('DataRetrieval/GetNames')
            .then(response => response.json() as Promise<string[]>)
            .then(data => {
                this.setState({ data: data, loading: false });
            });
    }

    public render() {
        return <h4> Profile Page </h4>
    }


}