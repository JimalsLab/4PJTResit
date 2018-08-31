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
        
        let itemlist = this.state.loading
            ? <p><em>Loading...</em></p>
            : <ul>{
                this.state.data.map(item =>
                    <li>{item.Name}</li>
                )
            }</ul>

        return <div>
            <h1>All Products</h1>
            {itemlist}
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