import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {

    public gotoItemList(event: any) {

    }

    public render() {
        return <div>
            <br />
            <br />
            <h2 className="row text-center"> GoodBurger : Mixing Complex and Subtle Tasty Burgers with Simple food delivery ! </h2>
            <br />
            <NavLink to={'/Cart'}>
                <div className="row tile tileheight featured_banner text-center">
                    <p className="featured_title">Featured</p>
                    <p className="description_text">*A weekly refreshed list of our suggestions !</p>
                </div>
            </NavLink>
            <div>
                <div className="col-md-6">
                    <NavLink to={'/Cart'}>
                        <div className="tile tileheight minitile">
                            <div className="col-md-5">
                                <img src="./img/MenuIcon.png" className="display_block" />
                            </div>
                            <div className="col-md-7">
                                <p>Menus</p>
                                <h5>Here are all promotionnal offers we have !</h5>
                            </div>
                        </div>
                    </NavLink> 
                </div>
                <div className="col-md-6">
                    <NavLink to={'/Items'}>
                        <div className="tile tileheight minitile">
                            <div className="col-md-5">
                                <img src="./img/ALaCarteIcon.png" className="display_block" />
                            </div>
                            <div className="col-md-7">
                                <p>All Items</p>
                                <h5>Pick everything you need here !</h5>
                            </div>
                        </div>
                    </NavLink>
                </div>
            </div>
            
        </div>;
    }
}
