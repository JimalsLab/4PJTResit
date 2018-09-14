import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface ProductData {
    data: Burgers[];
    loading: boolean;
}

export class Cart extends React.Component<RouteComponentProps<{}>, ProductData>
{
    constructor() {
        super();
        this.state = { data: [], loading: true };

        fetch('DataRetrieval/CartItems')
            .then(response => response.json() as Promise<Burgers[]>)
            .then(data => {
                this.setState({ data: data, loading: false });
            });
    }

    public render() {

        let itemlist = this.state.loading
            ? <p><em>Loading...</em></p>
            : Cart.renderTable(this.state.data);



        return <div>
            {itemlist}
        </div>;
    }

    public static renderTable()
}

interface Burgers {
    id: number;
    name: string;
    price: number;
    picture: string;
    number: number;
    idcart: number;
    type: string;
    children: string;
    description: string;
    components: string;
    cities: number;
}