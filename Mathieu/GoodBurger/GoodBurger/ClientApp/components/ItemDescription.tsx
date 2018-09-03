import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface ProductData {
    data: Burgers;
    loading: boolean;

}

export class ItemDescription extends React.Component<RouteComponentProps<{}>, ProductData>
{
    constructor() {
        super();
        this.state = {
            data: Object.create(null), loading: true
        };
        var url = window.location.href;
        var index = url.lastIndexOf('/');
        if (index > 0)
            url = url.substring(0, index + 1);
        const handle = this.props.match.params;
        fetch('api/DataRetrieval/Item/2')
            .then(response => response.json() as Promise<Burgers>)
            .then(data => {
                this.setState({ data: data, loading: false });
            });
    }

    public render() {
        return <div>
            <div className="col-md-6">
                <img src="./img/ALaCarteIcon.png" className="display_block" />
            </div>
            <div className="col-md-3">
                
                <p>{this.state.data.type}</p>
                <p>{this.state.data.description}</p>
                <p> Price : {this.state.data.price}</p>
            </div>
            <div className="col-md-3">
                <ul>
                {this.parseComposition(this.state.data).map(comp =>
                    <li>{comp}</li>
                 )}
                </ul>
            </div>
        </div>
    }

    public parseComposition(item: Burgers) {
        return item.components.split(" ");
    }
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
    oncart: number;
}