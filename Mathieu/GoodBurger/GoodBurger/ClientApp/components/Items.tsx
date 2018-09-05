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
                    <div className="col-md-6" id={forecast.id.toString() + forecast.name}>
                        <div type="button" data-toggle="modal" data-target={"#" + forecast.id.toString()}>
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
                        </div>
                        <div className="modal fade" id={forecast.id.toString()} role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div className="modal-dialog" role="document">
                                <div className="modal-content">
                                    <div className="modal-header">
                                        <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                        <h3 className="modal-title" id="exampleModalLabel">{forecast.name}</h3>
                                    </div>
                                    <div className="modal-body">
                                        <div className="row paddingleft">
                                            <p>{forecast.description}</p>
                                        </div>
                                        <div className="row paddingleft">
                                            <div className="col-md-4">
                                                <br />
                                                <p>Price: {forecast.price}</p>
                                                <p>In Stock: {forecast.number}</p>
                                                <p className={this.quantityinfostatus((forecast.number).toString())}>{this.quantityinfo((forecast.number).toString())}</p>
                                            </div>
                                            <div className="col-md-1"></div>
                                            <div className="col-md-4">
                                                <ul>
                                                    {this.parseComposition(forecast).map(comp =>
                                                        <li>{comp}</li>
                                                    )}
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="row" ref="errordiv"></div>
                                    <div className="modal-footer">
                                        <div className="input-group">
                                            <form action="/api/DataRetrieval/AddToCart">
                                                <input id="stock" name="stock" value={forecast.number} hidden/>
                                                <input id="itemid" name="itemid" value={forecast.id} hidden/>
                                                <input ref="productnb" name="productnb" type="text" className="form-control display_block small_input " value="1" placeholder="Nb" aria-label="Recipient's username" aria-describedby="basic-addon2" id="productnb" />
                                                <button className="btn btn-danger display_block" type="submit">Add To Cart</button>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                )}
            </div>
            
        </div>;
    }

    public static parseComposition(item: Burgers) {
        var temp = item.components.split(" ");
        let tmp2: string;
        let tmp: string;
        tmp2 = "";
        if (temp.length > 3) {
            tmp2 = "bread";
            for (tmp of temp) {
                switch (tmp) {
                    case "0":
                        tmp2 = tmp2 + " steak";
                        break;
                    case "1":
                        tmp2 = tmp2 + " cheese";
                        break;
                    case "2":
                        tmp2 = tmp2 + " steak";
                        break;
                    case "3":
                        tmp2 = tmp2 + " steak";
                        break;
                    case "4":
                        tmp2 = tmp2 + " steak";
                        break;
                    case "5":
                        tmp2 = tmp2 + " steak";
                        break;
                    case "6":
                        tmp2 = tmp2 + " steak";
                        break;
                    case "7":
                        tmp2 = tmp2 + " steak";
                        break;
                    default:
                        tmp = "";
                        break;
                }
            }
        }
        else {
            tmp2 = "";
        }
        
        return tmp2.split(" ");
    }

    public static quantityinfo(val: string) {
        if (val == "0") {
            return "OUT OF STOCK";
        }
        else {
            return "IN STOCK";
        }
    }

    public static quantityinfostatus(val: string) {
        if (val == "0") {
            return "redclass";
        }
        else {
            return "greenclass";
        }
    }

    public render() {
        
        let itemlist = this.state.loading
            ? <p><em>Loading...</em></p>
            : Items.renderTable(this.state.data);



        return <div>
            {itemlist}
        </div>;
    }

    public static rendererror() {
        var div = document.createElement('div');
        div.className = 'row';
        div.innerHTML = 
            '<p>erreur!</p>';
        return div;
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
    cities: number;
}

