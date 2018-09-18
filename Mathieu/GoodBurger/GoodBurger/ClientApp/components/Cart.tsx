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
            <br/>
            {itemlist}
        </div>;
    }

    public static renderTable(stuff: Burgers[]) {
        return <div>
            <br/>
            <div className="col-md-10">
                <div className="col-md-7">
                    <div>
                        {stuff.map(forecast =>
                            <div id={forecast.id.toString() + forecast.name}>
                                <div type="button" data-toggle="modal" data-target={"#" + forecast.id.toString()}>
                                    <div className="tile tileheight minitile tilettl">
                                        <div className="col-md-5">
                                            <img src="./img/ALaCarteIcon.png" className="display_block" />
                                        </div>
                                        <div className="col-md-7">
                                            <p className="smallfont">{forecast.name}</p>

                                            <h5>{forecast.description}
                                            </h5>
                                            <h5>Price : {forecast.price}$</h5>
                                            <b><h3 className="green">x {forecast.number}</h3></b>
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
                                                <h3 className="modal-title" id="exampleModalLabel">{forecast.number} {forecast.name}(s)</h3>
                                            </div>
                                            <div className="modal-footer">
                                                <div className="input-group">
                                                    <form id="deleteform" action="/DataRetrieval/DeleteCartItem">
                                                        <input id="id" name="id" value={forecast.id} hidden />
                                                        <button className="btn btn-danger display_block" type="submit">Delete</button>
                                                    </form>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        )}
                    </div>
                </div>
                <br />
                <div className="col-md-3">
                    <br />
                    <form id="Checkout" action="/DataRetrieval/Checkout">
                        <input id="id" name="id" value={stuff[0].idcart} hidden />
                        <button className="btn btn-lg btn-danger display_block" type="submit"><span className='glyphicon glyphicon-shopping-cart'></span> Checkout</button>
                    </form>
                </div>
            </div>


            </div>
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