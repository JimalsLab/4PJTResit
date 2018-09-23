import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface ProductData {
    data: Data;
    loading: boolean;
}

export class Cart extends React.Component<RouteComponentProps<{}>, ProductData>
{
    constructor() {
        super();
        this.state = { data: Object.create(null), loading: true };

        fetch('DataRetrieval/GetAdminPanelInfo')
            .then(response => response.json() as Promise<Data>)
            .then(data => {
                this.setState({ data: data, loading: false });
            });
    }

    public render() {

        let itemlist = this.state.loading
            ? <p><em>No Item in Cart</em></p>
            : Cart.renderPanel(this.state.data);



        return <div>
            <br />
            {itemlist}
        </div>;
    }

    public static renderPanel(panel: Data) {

    }
}

interface Data {
    menuBurgers: Product[];
    boughtBurgers: Product[];
    sales: number;
}

interface Product {
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