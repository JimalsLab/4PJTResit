import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import 'isomorphic-fetch';


interface UserData {
    data: User;
    loading: boolean;
}

export class Profile extends React.Component<RouteComponentProps<{}>, UserData>
{
    constructor() {
        super();
        this.state = { data: Object.create(null), loading: true };

        fetch('DataRetrieval/GetCurrentUser')
            .then(response => response.json() as Promise<User>)
            .then(data => {
                this.setState({ data: data, loading: false });
            });
    }

    public render() {
        let result = this.state.loading
            ? <div></div>
            : Profile.TestAdmin(this.state.data);

        return <div>
            <br />
            <br />
            <h2>Change profile</h2>
            <br />
            <form action="/DataRetrieval/UpdateOrRegister">
                <div className="col-md-5">
                    <div className="row">
                        <div className="col-md-4">
                            <p className="alignright">Username</p>
                        </div>
                        <div className="col-md-8">
                            <input className="form-control" id="username" name="username" />
                        </div>
                    </div>
                    <br />
                    <div className="row">
                        <div className="col-md-4">
                            <p className="alignright">Password</p>
                        </div>
                        <div className="col-md-8">
                            <input className="form-control" id="pass" name="password" type="password" />
                        </div>
                    </div>
                    <br />
                    <div className="row">
                        <div className="col-md-4">
                            <p className="alignright">Repeat Password</p>
                        </div>
                        <div className="col-md-8">
                            <input className="form-control" id="pass2" name="passwordrepeat" type="password" />
                        </div>
                    </div>
                    <br />
                    <div className="row">
                        <div className="col-md-4">
                            <p className="alignright">Address</p>
                        </div>
                        <div className="col-md-8">
                            <input className="form-control" id="address" name="address" />
                        </div>
                    </div>
                    <br />
                    <div className="row">
                        <div className="col-md-4">
                        </div>
                        <div className="col-md-8">
                            <input type="submit" className="btn btn-danger" id="address" value="Update Information" />
                        </div>
                    </div>
                </div>
            </form>

            

            <div className="col-md-2">
                {result}
            </div>
        </div>;
    }

    public static TestAdmin(dude : User) {
        if (dude != null) {
            if (dude.isAdmin >= 0) {
                return <div className="topright">
                    <NavLink to={'/AdminPanel'} exact activeClassName='active'>
                        <span className='glyphicon glyphicon-th'></span> Admin Panel
                    </NavLink>
                </div>
            }
        }
    }


}
interface User {
    id: number;
    username: string;
    password: string;
    address: string;
    name: string;
    isAdmin: number;
}