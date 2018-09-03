import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import 'isomorphic-fetch';

interface ProductData {
    data: Burgers[];
    loading: boolean;

}

export class Items extends React.Component<RouteComponentProps<{}>, ProductData>
{
    constructor() {
        super();
        this.state = { data: [], loading: true };

        fetch('api/DataRetrieval/GetProducts')
            .then(response => response.json() as Promise<Burgers[]>)
            .then(data => {
                this.setState({ data: data, loading: false });
            });
    }

    private static renderTable(stuff: Burgers[]) {
        return <div>
            <div className="text-center">
                <h2 className="minitile titlemargin">Our Products</h2>
            </div>
            <div>
                {stuff.map(forecast =>
                    <div className="col-md-6">
                        <NavLink to={'/Item/' + forecast.id}>
                            <div className="tile tileheight minitile tilettl">
                                <div className="col-md-5">
                                    <img src="./img/ALaCarteIcon.png" className="display_block" />
                                </div>
                                <div className="col-md-7">
                                    <p className="smallfont">{forecast.name}</p>

                                    <h5>{forecast.description}
                                    </h5>
                                    <h5>Price : {forecast.price}</h5>
                                </div>
                            </div>
                        </NavLink>
                    </div>
                )}
            </div>
            
        </div>;
    }

    public render() {
        
        let itemlist = this.state.loading
            ? <p><em>Loading...</em></p>
            : Items.renderTable(this.state.data);



        return <div>
            {itemlist}
        </div>;
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