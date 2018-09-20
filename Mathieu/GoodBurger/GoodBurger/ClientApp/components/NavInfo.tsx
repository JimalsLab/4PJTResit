import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import 'isomorphic-fetch';

interface ProductData {
    data: UserToken;
    loading: boolean;
}

interface UserToken {
    name: string;
    id: number;
    isadmin: number;
}

export class NavInfo extends React.Component<{}, ProductData>{
    constructor() {
        super();
        this.state = { data: Object.create(null), loading: true };

        fetch('DataRetrieval/GetUserToken')
            .then(response => response.json() as Promise<UserToken>)
            .then(data => {
                this.setState({ data: data, loading: false });
            });
    }

    public render() {

        let itemlist = this.state.loading
            ? <p><em>Loading...</em></p>
            : NavInfo.renderToken(this.state.data);


        return <div>
            {itemlist}
        </div>;
    }

    public static renderToken(token: UserToken) {

        let adminbutton = (token.isadmin >= 0)
            ? <NavLink to={'/AdminPanel'} exact activeClassName='active' className="adminbutton">
                <span className='glyphicon glyphicon-th'></span> Admin Panel
              </NavLink>
            : <div></div>

        let title = (token.id == -1)
            ? "Login" : "Logged in as "+ token.name;

        let form = (token.id == -1)
            ? <form className="row" id="login" action="/DataRetrieval/Login">
                <div className="row">
                    <div className="col-md-2 spaceup col-md-offset-1">
                        <div className="spaceup">Username</div>
                        <br />
                        <div >Password</div>
                    </div>
                    <div className="col-md-8">
                        <input className="form-control spaceup" id="username" name="username" type="text" placeholder="Username" />
                        <input className="form-control spaceup" id="password" name="password" type="password" placeholder="Password" />
                    </div>
                </div>
                
                <div className="row spaceup">
                    <div className="col-md-11">
                        {adminbutton}
                        <button className="btn btn-danger" type="submit" >Submit</button>
                    </div>
                </div>
            </form>

            : <form id="login" action="/DataRetrieval/Disconnect">
                {adminbutton}
                <button className="btn btn-danger" >Disconnect</button>
            </form>;

        return <div>
            <button className="btn btn-danger userinfo" data-toggle="modal" data-target={"#" + token.id.toString()}><span className="glyphicon glyphicon-user">&nbsp;</span>{token.name}</button>
            <div className="modal fade" id={token.id.toString()} role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h3 className="modal-title" id="exampleModalLabel">{title}</h3>
                        </div>
                        <div className="modal-footer">
                            {form}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    }
}