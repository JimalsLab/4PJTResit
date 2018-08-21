import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface ProductData {
    data: Products[];
    loading: boolean;
}

export class Items extends React.Component<RouteComponentProps<{}>, ProductData>
{
    constructor() {
        super();
        this.state = { data: [], loading: true };

        fetch('api/DataRetrieval/GetProducts')
            .then(response => response.json() as Promise<Products[]>)
            .then(data => {
                this.setState({ data: data, loading: false });
            });
    }

    public render() {
        
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <p>{this.state.data[0].Name}</p>

        return <div>
            <h1>All Products</h1>
            {contents}
            <ul>
                {this.state.data.map(item =>
                    <li key={item.Id}> { item.Name }</li>
            )}
            </ul>
        </div>;
    }

}

interface Products {
    Id: number;
    Name: string;
    Price: number;
    Picture: string;
    Number: number;
    IdCart: number;
    Type: string;
    Children: string;
    Description: string;
    Components: string;
    OnCart: number;
}