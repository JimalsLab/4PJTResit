import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div>
            <br />
            <br />
            <h2 className="row text-center"> GoodBurger : Mixing Complex and Subtle Tasty Burgers with Simple food delivery ! </h2>
            <br />
            <div className="row tile tileheight featured_banner text-center">
                <p className="featured_title">Featured</p>
                <p className="description_text">*A weekly refreshed list of our suggestions !</p>
            </div>
            <div>
                <div className="col-md-6">
                    <div className="tile tileheight">
                        <div className="col-md-5">
                            <img src="./img/placeholder.png" className="display_block" />
                        </div>
                        <div className="col-md-7">
                            <h2>Menus</h2>
                        </div>
                    </div>
                </div>
                <div className="col-md-6">
                    <div className="tile tileheight">
                        <div className="col-md-5">
                            <img src="./img/placeholder.png" className="display_block" />
                        </div>
                        <div className="col-md-7">
                            <h2>A la carte</h2>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>;
    }
}
